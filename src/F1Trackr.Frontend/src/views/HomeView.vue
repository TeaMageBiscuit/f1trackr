<script setup lang="ts">
import type { ConstructorSummary, DriverSummary, RaceSummary } from '@/models'
import SeasonBrowserPage from '@/pages/SeasonBrowserPage.vue'
import { useCache, useCachePool } from '@/utils/cache.ts'
import useHttp from '@/utils/http.ts'
import { useStorage } from '@vueuse/core'
import { nextTick, onMounted, ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const http = useHttp()
const router = useRouter()

const seasonsCache = useCache<number[]>('seasons', {
  ttl: 60 * 60 * 1000,
  storage: sessionStorage,
  initialValue: undefined,
})

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

const loading = ref(false)
const currentSeason = useStorage('currentSeason', new Date().getFullYear())
const currentTab = useStorage('home-tab', 'constructors')
const seasons = ref<number[]>([])
const constructors = ref<ConstructorSummary[]>([])
const drivers = ref<DriverSummary[]>([])
const races = ref<RaceSummary[]>([])

const refresh = async () => {
  loading.value = true

  await nextTick()

  try {
    const season = currentSeason.value
    seasons.value = await seasonsCache.get(() =>
      http.get<number[]>('/api/seasons').then((response) => response.value ?? [season]),
    )

    constructors.value = await constructorsCache.get(`${season}`, async () => {
      const response = await http.get<ConstructorSummary[]>(`/api/seasons/${season}/constructors`)
      return response.value ?? []
    })

    drivers.value = await driversCache.get(`${season}`, async () => {
      const response = await http.get<DriverSummary[]>(`/api/seasons/${season}/drivers`)
      return response.value ?? []
    })

    races.value = await racesCache.get(`${season}`, async () => {
      const response = await http.get<RaceSummary[]>(`/api/seasons/${season}/races`)
      return response.value ?? []
    })
  } finally {
    loading.value = false
  }
}

onMounted(refresh)
watch(currentSeason, refresh)

function viewRace(race: RaceSummary) {
  router.push({ name: 'race-details', params: { season: race.id.season, round: race.id.round } })
}
</script>

<template>
  <SeasonBrowserPage
    v-model:current-season="currentSeason"
    v-model:current-tab="currentTab"
    :seasons="seasons"
    :loading="loading"
    :constructors="constructors"
    :drivers="drivers"
    :races="races"
    @refresh="refresh"
    @select-race="viewRace"
  />
</template>
