<script setup lang="ts">
import DriverListItem from '@/components/DriverListItem.vue'
import RaceListItem from '@/components/RaceListItem.vue'
import type { GroupMember, RaceSummary } from '@/models'
import Button from 'primevue/button'
import Column from 'primevue/column'
import DataTable from 'primevue/datatable'
import Select from 'primevue/select'
import { computed, watch } from 'vue'

const selectedRound = defineModel<number | null>('selectedRound')

const props = defineProps<{
  loading: boolean
  canEdit: boolean
  canForceEdit: boolean
  canInteract: boolean
  member?: GroupMember
  races: RaceSummary[]
  round?: number
}>()

defineEmits<{
  (e: 'editRacePrediction', round: number): void
  (e: 'select-race', race: RaceSummary): void
}>()

// const selectedRound = ref<number | null>(props.round ?? null)
const racePredictions = computed(() => props.member?.driverRacePredictions ?? [])

const raceLookup = computed(() => {
  return Object.assign({}, ...props.races.map((c) => ({ [c.id.round]: c })))
})

const selectedRaceIndex = computed(() => {
  if (selectedRound.value === null) return -1
  return props.races.findIndex((r) => Number(r.id?.round) === Number(selectedRound.value))
})

const selectedRacePredictionIndex = computed(() => {
  if (selectedRound.value === null) return -1
  return racePredictions.value.findIndex((r) => Number(r.raceId?.round) === Number(selectedRound.value))
})

const selectedRacePrediction = computed(() => {
  const i = selectedRacePredictionIndex.value
  return i >= 0 ? racePredictions.value[i] : null
})

const selectedRaceSummary = computed(() => {
  const round = selectedRound.value
  if (round == null) return null
  return raceLookup.value[round] ?? null
})

const raceOptions = computed(() => {
  return props.races.map((r) => {
    const round = Number(r.id?.round)
    const name = r.name ?? 'Unknown race'
    return { label: `Round ${round} — ${name}`, value: round }
  })
})

const canEditRace = computed(() => {
  const today = new Date()
  const raceDate = new Date(selectedRaceSummary.value?.firstPracticeTime ?? selectedRaceSummary.value?.grandPrixTime)
  return props.canForceEdit || (props.canEdit && today <= raceDate)
})

const canGoPrev = computed(() => selectedRaceIndex.value > 0)
const canGoNext = computed(() => selectedRaceIndex.value >= 0 && selectedRaceIndex.value < props.races.length - 1)

const goPrev = () => {
  if (!canGoPrev.value) return
  const prev = props.races[selectedRaceIndex.value - 1]
  selectedRound.value = Number(prev?.id?.round)
}

const goNext = () => {
  if (!canGoNext.value) return
  const next = props.races[selectedRaceIndex.value + 1]
  selectedRound.value = Number(next?.id?.round)
}

watch(
  () => props.races,
  () => {
    const today = new Date()
    const next = props.races.find((r) => {
      const raceTime = new Date(r.grandPrixTime)
      return today <= raceTime
    })

    if (!selectedRound.value) {
      const last = props.races[props.races.length - 1]
      selectedRound.value = next ? Number(next.id?.round) : last ? Number(last.id?.round) : null
    }
  },
  { immediate: true },
)
</script>

<template>
  <div v-if="!races?.length" class="text-surface-600 dark:text-surface-300">No race predictions found.</div>

  <div v-else>
    <div class="flex items-center justify-between gap-3 mb-3">
      <div class="flex items-center gap-2 w-full max-w-xl mx-auto">
        <Button
          icon="pi pi-chevron-left"
          label="Prev"
          severity="secondary"
          outlined
          size="small"
          :disabled="!canInteract || !canGoPrev"
          @click="goPrev"
        />

        <div class="min-w-0 flex-1">
          <Select
            v-model="selectedRound"
            :options="raceOptions"
            option-label="label"
            option-value="value"
            :disabled="!canInteract"
            placeholder="Select a race"
            class="w-full"
          />
        </div>

        <Button
          icon="pi pi-chevron-right"
          label="Next"
          severity="secondary"
          outlined
          size="small"
          :disabled="!canInteract || !canGoNext"
          @click="goNext"
        />

        <Button
          v-if="canEditRace"
          icon="pi pi-pencil"
          label="Edit"
          size="small"
          :disabled="!canInteract || selectedRound == null"
          @click="$emit('editRacePrediction', Number(selectedRound))"
        />
      </div>
    </div>

    <div class="flex justify-center mb-3">
      <div class="w-full max-w-xl">
        <RaceListItem
          v-if="selectedRaceSummary"
          :race="selectedRaceSummary"
          :emphasize="true"
          @select="$emit('select-race', $event)"
        />
        <div v-else class="text-surface-600 dark:text-surface-300 text-sm text-center">
          Select a race to view predictions.
        </div>
      </div>
    </div>

    <DataTable
      :value="selectedRacePrediction?.drivers ?? []"
      :loading="loading"
      striped-rows
      show-gridlines
      size="small"
      data-key="driver.driverId.value"
    >
      <template #empty>
        <div class="p-2 text-surface-600 dark:text-surface-300">No drivers predicted for this race.</div>
      </template>

      <Column header="#" class="w-18">
        <template #body="{ index }">{{ index + 1 }}</template>
      </Column>

      <Column header="Driver">
        <template #body="{ data }">
          <DriverListItem :driver="data.driver" :emphasize="false" />
        </template>
      </Column>
    </DataTable>
  </div>
</template>
