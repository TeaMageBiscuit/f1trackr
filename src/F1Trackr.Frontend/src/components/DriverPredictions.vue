<script setup lang="ts">
import DriverListItem from '@/components/DriverListItem.vue'
import type { GroupMember } from '@/models'
import Button from 'primevue/button'
import Column from 'primevue/column'
import DataTable from 'primevue/datatable'

defineProps<{
  loading: boolean
  canInteract: boolean
  canEditSeasonStandings: boolean
  member?: GroupMember
}>()

defineEmits<{
  (e: 'editDriverStandingsPrediction'): void
}>()
</script>

<template>
  <div>
    <div class="flex justify-between items-center mb-3">
      <div class="font-medium">Drivers</div>
      <Button
        v-if="canEditSeasonStandings"
        icon="pi pi-pencil"
        label="Edit"
        size="small"
        :disabled="!canInteract"
        @click="$emit('editDriverStandingsPrediction')"
      />
    </div>

    <DataTable
      :value="member?.driverPredictions ?? []"
      :loading="loading"
      striped-rows
      show-gridlines
      size="small"
      data-key="driver.driverId.value"
    >
      <template #empty>
        <div class="p-2 text-surface-600 dark:text-surface-300">No driver predictions found.</div>
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
