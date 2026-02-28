<script setup lang="ts">
import type { RaceId } from '@/models'
import Button from 'primevue/button'
import InputNumber from 'primevue/inputnumber'
import Select from 'primevue/select'
import { ref } from 'vue'

defineProps<{
  loading: boolean
  seasons: number[]
}>()

defineEmits<{
  (e: 'import-constructors', season: number): void
  (e: 'import-drivers', season: number): void
  (e: 'import-races', season: number): void
  (e: 'import-race-results', raceId: RaceId): void
  (e: 'recalculate-scores', raceId: RaceId): void
}>()

const season = ref<number>(new Date().getFullYear())
const round = ref<number>(1)
</script>

<template>
  <div>
    <div class="text-surface-600 dark:text-surface-300">Import Tools</div>

    <div class="flex flex-row gap-2 mt-4">
      <div class="flex flex-col gap-2 w-1/3">
        <div class="flex items-center gap-2">
          <label class="w-1/5">Season</label>
          <Select v-model="season" :options="seasons" :disabled="loading" fluid />
        </div>
        <div class="flex items-center gap-2">
          <label class="w-1/5">Round</label>
          <InputNumber v-model="round" show-buttons :min="1" :max="30" :disabled="loading" fluid />
        </div>
      </div>

      <div class="w-2/3 flex flex-row gap-2">
        <Button
          :disabled="loading"
          class="w-full"
          label="Import Constructors"
          size="large"
          @click="$emit('import-constructors', season!)"
        />
        <Button
          :disabled="loading"
          class="w-full"
          label="Import Drivers"
          size="large"
          @click="$emit('import-drivers', season!)"
        />
        <Button
          :disabled="loading"
          class="w-full"
          label="Import Races"
          size="large"
          @click="$emit('import-races', season!)"
        />
        <Button
          :disabled="loading"
          class="w-full"
          label="Import Race Results"
          size="large"
          @click="$emit('import-race-results', { season: season.toString(), round } as RaceId)"
        />
        <Button
          :disabled="loading"
          class="w-full"
          label="Recalculate Scores"
          size="large"
          @click="$emit('recalculate-scores', { season: season.toString(), round } as RaceId)"
        />
      </div>
    </div>
  </div>
</template>
