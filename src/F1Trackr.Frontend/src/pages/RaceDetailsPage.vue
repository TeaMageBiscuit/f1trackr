<script setup lang="ts">
import ConstructorListItem from '@/components/ConstructorListItem.vue'
import DriverListItem from '@/components/DriverListItem.vue'
import type { ConstructorSummary, DriverSummary, RaceOverview } from '@/models'
import { parseLapTime } from '@/utils/time.ts'
import PrimeVueButton from 'primevue/button'
import Column from 'primevue/column'
import DataTable from 'primevue/datatable'
import Skeleton from 'primevue/skeleton'
import Tab from 'primevue/tab'
import TabList from 'primevue/tablist'
import TabPanel from 'primevue/tabpanel'
import TabPanels from 'primevue/tabpanels'
import Tabs from 'primevue/tabs'
import { computed } from 'vue'

const props = defineProps<{
  loading: boolean
  constructors: ConstructorSummary[]
  drivers: DriverSummary[]
  race: RaceOverview | null
}>()

defineEmits(['back'])

const constructorLookup = computed(() => {
  return Object.assign({}, ...props.constructors.map((c) => ({ [c.id.value]: c })))
})

const driverLookup = computed(() => {
  return Object.assign({}, ...props.drivers.map((d) => ({ [d.id.value]: d })))
})

const formatDate = (dateStr?: string | Date | null) => {
  if (!dateStr) return ''

  return new Date(dateStr).toLocaleDateString('en-GB', {
    day: 'numeric',
    month: 'short',
    year: 'numeric',
    hour: 'numeric',
    minute: 'numeric',
  })
}

const sessionTimes = computed(() => {
  const r = props.race

  return [
    { label: 'FP1', value: r?.firstPracticeTime, formatted: formatDate(r?.firstPracticeTime) },
    { label: 'FP2', value: r?.secondPracticeTime, formatted: formatDate(r?.secondPracticeTime) },
    { label: 'FP3', value: r?.thirdPracticeTime, formatted: formatDate(r?.thirdPracticeTime) },
    { label: 'Sprint Qualifying', value: r?.sprintQualifyingTime, formatted: formatDate(r?.sprintQualifyingTime) },
    { label: 'Sprint', value: r?.sprintTime, formatted: formatDate(r?.sprintTime) },
    { label: 'Qualifying', value: r?.qualifyingTime, formatted: formatDate(r?.qualifyingTime) },
    { label: 'Grand Prix', value: r?.grandPrixTime, formatted: formatDate(r?.grandPrixTime) },
  ].filter((x) => !!x.value)
})

const raceSummary = computed(() => {
  const r = props.race

  return [
    {
      label: 'Race Winner',
      value: r?.driverResults?.[0]?.driverId.value,
      formatted: (driverId: string) =>
        `${driverLookup.value[driverId].givenName} ${driverLookup.value[driverId].familyName}`,
    },
    {
      label: 'Second Place',
      value: r?.driverResults?.[1]?.driverId.value,
      formatted: (driverId: string) =>
        `${driverLookup.value[driverId].givenName} ${driverLookup.value[driverId].familyName}`,
    },
    {
      label: 'Third Place',
      value: r?.driverResults?.[2]?.driverId.value,
      formatted: (driverId: string) =>
        `${driverLookup.value[driverId].givenName} ${driverLookup.value[driverId].familyName}`,
    },
    {
      label: 'Fastest Lap',
      value: r?.driverResults
        ?.map((dr) => ({ driverId: dr.driverId, lapTime: parseLapTime(dr.fastestLapTime) ?? Number.MAX_SAFE_INTEGER }))
        .sort((a, b) => a.lapTime - b.lapTime)[0]?.driverId.value,
      formatted: (driverId: string) =>
        `${driverLookup.value[driverId].givenName} ${driverLookup.value[driverId].familyName}`,
    },
    {
      label: 'Fastest Qualifying Lap',
      value: r?.qualifyingResults
        ?.map((qr) => ({ driverId: qr.driverId, lapTime: parseLapTime(qr.q3) ?? Number.MAX_SAFE_INTEGER }))
        .sort((a, b) => a.lapTime - b.lapTime)[0]?.driverId.value,
      formatted: (driverId: string) =>
        `${driverLookup.value[driverId].givenName} ${driverLookup.value[driverId].familyName}`,
    },
    {
      label: 'Sprint Winner',
      value: r?.sprintDriverResults?.[0]?.driverId.value,
      formatted: (driverId: string) =>
        `${driverLookup.value[driverId].givenName} ${driverLookup.value[driverId].familyName}`,
    },
    {
      label: 'Fastest Sprint Lap',
      value: r?.sprintDriverResults
        ?.map((sr) => ({ driverId: sr.driverId, lapTime: parseLapTime(sr.fastestLapTime) ?? Number.MAX_SAFE_INTEGER }))
        .sort((a, b) => a.lapTime - b.lapTime)[0]?.driverId.value,
      formatted: (driverId: string) =>
        `${driverLookup.value[driverId].givenName} ${driverLookup.value[driverId].familyName}`,
    },
  ].filter((x) => !!x.value)
})
</script>

<template>
  <div class="p-6 max-w-7xl mx-auto">
    <div class="flex items-center gap-4 mb-6">
      <PrimeVueButton icon="pi pi-arrow-left" severity="secondary" rounded variant="text" @click="$emit('back')" />
      <div>
        <h1 class="text-3xl font-bold">{{ race?.name }}</h1>
        <p class="text-surface-500">
          {{ race?.circuit }} •
          {{ new Date(race?.grandPrixTime ?? '').getFullYear() }}
        </p>
      </div>
    </div>

    <div class="grid grid-cols-2 gap-4 mb-4">
      <div class="card p-4!">
        <div class="flex items-center justify-between mb-3">
          <div class="font-semibold">Session Times</div>
        </div>

        <div class="flex flex-col gap-2">
          <template v-if="loading">
            <Skeleton height="1.25rem" />
            <Skeleton height="1.25rem" />
            <Skeleton height="1.25rem" />
            <Skeleton height="1.25rem" />
            <Skeleton height="1.25rem" />
          </template>

          <template v-else>
            <div v-for="s in sessionTimes" :key="s.label" class="flex items-center justify-between gap-4">
              <div class="text-surface-600 dark:text-surface-300 text-sm">
                {{ s.label }}
              </div>
              <div class="text-xs text-surface-500 font-mono text-right">
                {{ s.formatted || '—' }}
              </div>
            </div>

            <div v-if="!sessionTimes.length" class="text-surface-600 dark:text-surface-300 text-sm">—</div>
          </template>
        </div>
      </div>

      <div class="card p-4!">
        <div class="flex items-center justify-between mb-3">
          <div class="font-semibold">Race Summary</div>
        </div>

        <div class="flex flex-col gap-2">
          <template v-if="loading">
            <Skeleton height="1.25rem" />
            <Skeleton height="1.25rem" />
            <Skeleton height="1.25rem" />
            <Skeleton height="1.25rem" />
            <Skeleton height="1.25rem" />
          </template>

          <template v-else>
            <div v-for="s in raceSummary" :key="s.label" class="flex items-center justify-between gap-4">
              <div class="text-surface-600 dark:text-surface-300 text-sm">
                {{ s.label }}
              </div>
              <div class="text-xs text-surface-500 font-mono text-right">
                {{ s.formatted(s.value!) || '—' }}
              </div>
            </div>

            <div v-if="!sessionTimes.length" class="text-surface-600 dark:text-surface-300 text-sm">—</div>
          </template>
        </div>
      </div>
    </div>

    <div class="card mb-4">
      <Tabs value="race">
        <TabList>
          <Tab value="race" :disabled="loading">Race Results</Tab>
          <Tab value="qualifying" :disabled="loading">Qualifying</Tab>
          <Tab v-if="race?.sprintTime" value="sprint" :disabled="loading">Sprint</Tab>
        </TabList>
        <TabPanels>
          <TabPanel value="race">
            <DataTable :value="race?.driverResults" :loading="loading" striped-rows size="small">
              <template #empty>
                <div class="p-2 text-surface-600 dark:text-surface-300">No race results.</div>
              </template>

              <Column field="position" header="Pos" class="w-16 font-bold" />
              <Column header="Driver">
                <template #body="{ data }">
                  <DriverListItem :driver="driverLookup[data.driverId.value]" :emphasize="false" />
                </template>
              </Column>
              <Column header="Constructor">
                <template #body="{ data }">
                  <ConstructorListItem :constructor="constructorLookup[data.constructorId.value]" :emphasize="false" />
                </template>
              </Column>
              <Column field="status" header="Status" />
              <Column field="points" header="Pts" class="font-bold" />
              <Column field="laps" header="Laps" />
              <Column field="totalTime" header="Total Time" class="text-right w-24" />
              <Column field="fastestLapTime" header="Fastest Lap" class="text-right w-24" />
            </DataTable>
          </TabPanel>

          <TabPanel value="qualifying">
            <DataTable :value="race?.qualifyingResults" :loading="loading" striped-rows size="small">
              <template #empty>
                <div class="p-2 text-surface-600 dark:text-surface-300">No qualifying results.</div>
              </template>

              <Column field="position" header="Pos" class="w-16 font-bold" />
              <Column header="Driver">
                <template #body="{ data }">
                  <DriverListItem :driver="driverLookup[data.driverId.value]" :emphasize="false" />
                </template>
              </Column>
              <Column header="Constructor">
                <template #body="{ data }">
                  <ConstructorListItem :constructor="constructorLookup[data.constructorId.value]" :emphasize="false" />
                </template>
              </Column>
              <Column field="q1" header="Q1 Time" class="text-right w-24" />
              <Column field="q2" header="Q2 Time" class="text-right w-24" />
              <Column field="q3" header="Q3 Time" class="text-right w-24" />
            </DataTable>
          </TabPanel>

          <TabPanel v-if="race?.sprintTime" value="sprint">
            <DataTable :value="race?.sprintDriverResults" :loading="loading" striped-rows size="small">
              <template #empty>
                <div class="p-2 text-surface-600 dark:text-surface-300">No sprint race results.</div>
              </template>

              <Column field="position" header="Pos" class="w-16 font-bold" />
              <Column header="Driver">
                <template #body="{ data }">
                  <DriverListItem :driver="driverLookup[data.driverId.value]" :emphasize="false" />
                </template>
              </Column>
              <Column header="Constructor">
                <template #body="{ data }">
                  <ConstructorListItem :constructor="constructorLookup[data.constructorId.value]" :emphasize="false" />
                </template>
              </Column>
              <Column field="status" header="Status" />
              <Column field="points" header="Pts" class="font-bold" />
              <Column field="laps" header="Laps" />
              <Column field="totalTime" header="Total Time" class="text-right w-24" />
              <Column field="fastestLapTime" header="Fastest Lap" class="text-right w-24" />
            </DataTable>
          </TabPanel>
        </TabPanels>
      </Tabs>
    </div>
  </div>
</template>
