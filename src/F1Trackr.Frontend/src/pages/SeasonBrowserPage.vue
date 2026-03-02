<script setup lang="ts">
import ConstructorsPanel from '@/components/ConstructorsPanel.vue'
import DriversPanel from '@/components/DriversPanel.vue'
import RacesPanel from '@/components/RacesPanel.vue'
import type { ConstructorSummary, DriverSummary, RaceSummary } from '@/models'
import Button from 'primevue/button'
import Select from 'primevue/select'
import Tab from 'primevue/tab'
import TabList from 'primevue/tablist'
import TabPanel from 'primevue/tabpanel'
import TabPanels from 'primevue/tabpanels'
import Tabs from 'primevue/tabs'

const currentSeason = defineModel<number>('currentSeason')
const currentTab = defineModel<string | number>('currentTab', { default: 'constructors' })

defineProps<{
  loading: boolean
  seasons: number[]
  constructors: ConstructorSummary[]
  drivers: DriverSummary[]
  races: RaceSummary[]
}>()

defineEmits<{
  (e: 'refresh'): void
  (e: 'select-race', race: RaceSummary): void
}>()
</script>

<template>
  <div class="p-6 max-w-7xl mx-auto">
    <header class="flex justify-between items-center mb-6">
      <h1 class="text-3xl font-bold text-primary">F1 Season Browser</h1>
      <div class="flex items-center gap-3">
        <label for="season" class="font-medium">Season:</label>
        <Select v-model="currentSeason" :options="seasons" :disabled="loading" />
        <Button
          icon="pi pi-refresh"
          label="Refresh"
          severity="secondary"
          outlined
          :loading="loading"
          @click="$emit('refresh')"
        />
      </div>
    </header>

    <div class="card">
      <Tabs :value="currentTab" @update:value="currentTab = $event">
        <TabList>
          <Tab value="constructors" :disabled="loading">Constructors</Tab>
          <Tab value="drivers" :disabled="loading">Drivers</Tab>
          <Tab value="races" :disabled="loading">Races</Tab>
        </TabList>
        <TabPanels>
          <!-- Constructors Panel -->
          <TabPanel value="constructors">
            <ConstructorsPanel :loading="loading" :constructors="constructors" />
          </TabPanel>

          <!-- Drivers Panel -->
          <TabPanel value="drivers">
            <DriversPanel :loading="loading" :drivers="drivers" />
          </TabPanel>

          <!-- Races Panel -->
          <TabPanel value="races">
            <RacesPanel :loading="loading" :races="races" @select="$emit('select-race', $event)" />
          </TabPanel>
        </TabPanels>
      </Tabs>
    </div>
  </div>
</template>
