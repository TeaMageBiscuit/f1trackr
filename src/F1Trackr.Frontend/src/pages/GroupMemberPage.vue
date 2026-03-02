<script setup lang="ts">
import ConstructorPredictions from '@/components/ConstructorPredictions.vue'
import DriverPredictions from '@/components/DriverPredictions.vue'
import RacePredictions from '@/components/RacePredictions.vue'
import ScoreHistory from '@/components/ScoreHistory.vue'
import type { GroupMember, RaceSummary } from '@/models'
import Button from 'primevue/button'
import Skeleton from 'primevue/skeleton'
import Tab from 'primevue/tab'
import TabList from 'primevue/tablist'
import TabPanel from 'primevue/tabpanel'
import TabPanels from 'primevue/tabpanels'
import Tabs from 'primevue/tabs'
import { computed } from 'vue'

const selectedRound = defineModel<number | null>('selectedRound')

const props = defineProps<{
  loading: boolean
  canEdit: boolean
  canForceEdit: boolean
  member?: GroupMember
  races: RaceSummary[]
}>()

defineEmits<{
  (e: 'refresh'): void
  (e: 'back'): void
  (e: 'editRacePrediction', round: number): void
  (e: 'editConstructorStandingsPrediction'): void
  (e: 'editDriverStandingsPrediction'): void
  (e: 'select-race', race: RaceSummary): void
}>()

const canInteract = computed(() => !props.loading && !!props.member)

const canEditSeasonStandings = computed(() => {
  const today = new Date()
  const firstRace = props.races[0]
  const firstRaceDate = new Date(firstRace?.firstPracticeTime ?? firstRace?.grandPrixTime ?? today)
  return props.canForceEdit || (props.canEdit && today < firstRaceDate)
})
</script>

<template>
  <div class="p-6 max-w-7xl mx-auto">
    <header class="flex justify-between items-center mb-6">
      <div>
        <h1 class="text-3xl font-bold text-primary">Group Member</h1>
        <p class="text-surface-600 dark:text-surface-300 mt-1">
          <Skeleton v-if="loading" height="1.55rem" width="16rem" />
          <span v-else-if="member"> {{ member.user?.name ?? 'Member' }} · Score {{ member.currentScore }} </span>
          <span v-else>Overview</span>
        </p>
      </div>

      <div class="flex items-center gap-2">
        <Button
          icon="pi pi-arrow-left"
          label="Back"
          severity="secondary"
          outlined
          :disabled="loading"
          @click="$emit('back')"
        />
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

    <div class="flex flex-col gap-4">
      <div class="card p-4!">
        <div class="flex justify-between items-center mb-3">
          <div class="font-medium">Score History</div>
          <div class="text-surface-600 dark:text-surface-300">{{ member?.scoreSnapshots?.length ?? 0 }} total</div>
        </div>
        <ScoreHistory :loading="loading" :member="member" />
      </div>

      <div class="card p-4!">
        <Tabs value="races">
          <TabList>
            <Tab value="races">Race Predictions</Tab>
            <Tab value="season">Season Predictions</Tab>
          </TabList>

          <TabPanels>
            <TabPanel value="races">
              <RacePredictions
                v-model:selected-round="selectedRound"
                :loading="loading"
                :can-edit="canEdit"
                :can-force-edit="canForceEdit"
                :can-interact="canInteract"
                :member="member"
                :races="races"
                @edit-race-prediction="$emit('editRacePrediction', $event)"
                @select-race="$emit('select-race', $event)"
              />
            </TabPanel>

            <TabPanel value="season">
              <div class="grid grid-cols-2 gap-4">
                <ConstructorPredictions
                  :loading="loading"
                  :can-interact="canInteract"
                  :can-edit-season-standings="canEditSeasonStandings"
                  :member="member"
                  @edit-constructor-standings-prediction="$emit('editConstructorStandingsPrediction')"
                />

                <DriverPredictions
                  :loading="loading"
                  :can-interact="canInteract"
                  :can-edit-season-standings="canEditSeasonStandings"
                  :member="member"
                  @edit-driver-standings-prediction="$emit('editDriverStandingsPrediction')"
                />
              </div>
            </TabPanel>
          </TabPanels>
        </Tabs>
      </div>
    </div>
  </div>
</template>
