<script setup lang="ts">
import type { ConstructorSummary, DriverSummary, RaceOverview } from '@/models'
import RaceDetailsPage from '@/pages/RaceDetailsPage.vue'
import { useCachePool } from '@/utils/cache.ts'
import useHttp from '@/utils/http.ts'
import { nextTick, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'

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

const loading = ref(false)
const race = ref<RaceOverview | null>(null)
const constructors = ref<ConstructorSummary[]>([])
const drivers = ref<DriverSummary[]>([])

const refresh = async () => {
  loading.value = true

  await nextTick()

  const season = route.params.season as string
  const round = route.params.round as string

  try {
    constructors.value = await constructorsCache.get(`${season}`, async () => {
      const response = await http.get<ConstructorSummary[]>(`/api/seasons/${season}/constructors`)
      return response.value ?? []
    })

    drivers.value = await driversCache.get(`${season}`, async () => {
      const response = await http.get<DriverSummary[]>(`/api/seasons/${season}/drivers`)
      return response.value ?? []
    })

    const raceResult = await http.get<RaceOverview>(`/api/seasons/${season}/races/${round}`)
    if (raceResult.success) {
      race.value = raceResult.value!
    } else {
      race.value = null
    }
  } finally {
    loading.value = false
  }
}

const goBack = () => {
  router.back()
}

onMounted(refresh)
</script>

<template>
  <RaceDetailsPage :loading="loading" :race="race" :constructors="constructors" :drivers="drivers" @back="goBack" />
</template>
