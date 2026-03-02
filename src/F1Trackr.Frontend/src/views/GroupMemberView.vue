<script setup lang="ts">
import ConstructorPredictionDialog from '@/dialogs/ConstructorPredictionDialog.vue'
import DriverPredictionDialog from '@/dialogs/DriverPredictionDialog.vue'
import RacePredictionDialog from '@/dialogs/RacePredictionDialog.vue'
import type { Account, ConstructorSummary, DriverSummary, GroupMember, GroupOverview, RaceSummary } from '@/models'
import GroupMemberPage from '@/pages/GroupMemberPage.vue'
import { useCachePool } from '@/utils/cache.ts'
import useHttp from '@/utils/http.ts'
import { useStorage } from '@vueuse/core'
import { useDialog, useToast } from 'primevue'
import { nextTick, onMounted, ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const dialog = useDialog()
const toast = useToast()
const http = useHttp()
const route = useRoute()
const router = useRouter()

const constructorsCache = useCachePool<ConstructorSummary[]>({
  ttl: 60 * 60 * 1000,
  storage: sessionStorage,
  initialValue: undefined,
  keyPrefix: 'constructors',
})

const driversCache = useCachePool<DriverSummary[]>({
  ttl: 60 * 60 * 1000,
  storage: sessionStorage,
  initialValue: undefined,
  keyPrefix: 'drivers',
})

const racesCache = useCachePool<RaceSummary[]>({
  ttl: 60 * 60 * 1000,
  storage: sessionStorage,
  initialValue: undefined,
  keyPrefix: 'races',
})

const account = ref<Account>({ isAuthenticated: false })
const loading = ref(false)
const races = ref<RaceSummary[]>([])
const group = ref<GroupOverview | undefined>(undefined)
const groupMember = ref<GroupMember | undefined>(undefined)
const groupMemberRound = useStorage<number | null>('groupMemberRound', null, undefined, {
  serializer: {
    read: (v: unknown) => (v ? Number(v) : null),
    write: (v: unknown) => JSON.stringify(v),
  },
})

const refresh = async () => {
  const groupId = String(route.params.groupId ?? '').trim()
  const userId = String(route.params.userId ?? '').trim()
  if (!groupId || !userId) {
    groupMember.value = undefined
    return
  }

  loading.value = true
  await nextTick()

  try {
    const result = await http.get<GroupMember>(`/api/groups/${groupId}/members/${userId}`)
    if (result.success) {
      groupMember.value = result.value!

      const groupResult = await http.get<GroupOverview>(`/api/groups/${groupId}`)

      if (groupResult.success) {
        group.value = groupResult.value!
      }

      races.value = await racesCache.get(`${group.value!.season}`, async () => {
        const response = await http.get<RaceSummary[]>(`/api/seasons/${group.value!.season}/races`)
        return response.value ?? []
      })
    } else {
      groupMember.value = undefined
    }
  } finally {
    loading.value = false
  }
}

const goBack = () => {
  router.back()
}

async function showConstructorPredictionDialog() {
  let constructors = groupMember.value?.constructorPredictions.map((p) => ({
    id: p.constructor.id,
    name: p.constructor.name,
    nationality: p.constructor.nationality,
    logoAddress: p.constructor.logoAddress,
  }))

  if (constructors === null || constructors === undefined || constructors.length === 0) {
    constructors = await constructorsCache.get(`${group.value!.season}`, async () => {
      const response = await http.get<ConstructorSummary[]>(`/api/seasons/${group.value!.season}/constructors`)
      return response.value ?? []
    })
  }

  dialog.open(ConstructorPredictionDialog, {
    props: {
      header: 'Update Constructor Prediction',
      closable: true,
      modal: true,
      contentClass: 'w-120',
    },
    data: {
      endpoint: `/api/groups/${group.value!.id.value}/constructors`,
      season: group.value?.season,
      constructors: constructors,
    },
    onClose: (options) => {
      const data = options?.data as { added?: boolean } | undefined
      if (data?.added) {
        toast.add({
          severity: 'success',
          summary: 'Constructor Prediction Updated',
          detail: 'Constructor prediction was updated successfully.',
          life: 3000,
        })
        refresh()
      }
    },
  })
}

async function showDriverPredictionDialog() {
  let drivers = groupMember.value?.driverPredictions.map((p) => ({
    id: p.driver.id,
    givenName: p.driver.givenName,
    familyName: p.driver.familyName,
    nationality: p.driver.nationality,
    code: p.driver.code,
    driverNumber: p.driver.driverNumber,
    imageAddress: p.driver.imageAddress,
  }))

  if (drivers === null || drivers === undefined || drivers.length === 0) {
    drivers = await driversCache.get(`${group.value!.season}`, async () => {
      const response = await http.get<DriverSummary[]>(`/api/seasons/${group.value!.season}/drivers`)
      return response.value ?? []
    })
  }

  dialog.open(DriverPredictionDialog, {
    props: {
      header: 'Update Driver Prediction',
      closable: true,
      modal: true,
      contentClass: 'w-120',
    },
    data: {
      endpoint: `/api/groups/${group.value!.id.value}/drivers`,
      season: group.value?.season,
      drivers: drivers,
    },
    onClose: (options) => {
      const data = options?.data as { added?: boolean } | undefined
      if (data?.added) {
        toast.add({
          severity: 'success',
          summary: 'Driver Prediction Updated',
          detail: 'Driver prediction was updated successfully.',
          life: 3000,
        })
        refresh()
      }
    },
  })
}

async function showRacePredictionDialog(round: number) {
  const racePrediction = groupMember.value?.driverRacePredictions.find((p) => p.raceId.round === round)

  const predictions =
    racePrediction?.drivers.map((p) => ({
      id: p.driver.id,
      givenName: p.driver.givenName,
      familyName: p.driver.familyName,
      nationality: p.driver.nationality,
      code: p.driver.code,
      driverNumber: p.driver.driverNumber,
      imageAddress: p.driver.imageAddress,
    })) ?? []

  const allDrivers = await driversCache.get(`${group.value!.season}`, async () => {
    const response = await http.get<DriverSummary[]>(`/api/seasons/${group.value!.season}/drivers`)
    return response.value ?? []
  })

  const selected = predictions.map((p) => p.id.value)
  const remainingDrivers = allDrivers.filter((d) => !selected.includes(d.id.value))

  dialog.open(RacePredictionDialog, {
    props: {
      header: 'Update Race Prediction',
      closable: true,
      modal: true,
      contentClass: 'min-w-150 w-250',
    },
    data: {
      endpoint: `/api/groups/${group.value!.id.value}/races/${round}`,
      season: group.value?.season,
      drivers: [remainingDrivers, predictions],
    },
    onClose: (options) => {
      const data = options?.data as { added?: boolean } | undefined
      if (data?.added) {
        toast.add({
          severity: 'success',
          summary: 'Race Prediction Updated',
          detail: 'Race prediction was updated successfully.',
          life: 3000,
        })
        refresh()
      }
    },
  })
}

function viewRace(race: RaceSummary) {
  router.push({ name: 'race-details', params: { season: race.id.season, round: race.id.round } })
}

onMounted(async () => {
  account.value = await http
    .get<Account>('/api/account/me')
    .then((response) => response.value ?? { isAuthenticated: false })

  await refresh()
})

watch(
  () => route.params.groupId,
  async () => {
    await refresh()
  },
)
</script>

<template>
  <GroupMemberPage
    v-model:selected-round="groupMemberRound"
    :loading="loading"
    :can-edit="groupMember?.userId.value === account.userId"
    :can-force-edit="false"
    :member="groupMember"
    :races="races"
    @refresh="refresh"
    @back="goBack"
    @edit-race-prediction="showRacePredictionDialog"
    @edit-constructor-standings-prediction="showConstructorPredictionDialog"
    @edit-driver-standings-prediction="showDriverPredictionDialog"
    @select-race="viewRace"
  />
</template>
