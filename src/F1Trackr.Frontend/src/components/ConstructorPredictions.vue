<script setup lang="ts">
import ConstructorListItem from '@/components/ConstructorListItem.vue'
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
  (e: 'editConstructorStandingsPrediction'): void
}>()
</script>

<template>
  <div>
    <div class="flex justify-between items-center mb-3">
      <div class="font-medium">Constructors</div>
      <Button
        v-if="canEditSeasonStandings"
        icon="pi pi-pencil"
        label="Edit"
        size="small"
        :disabled="!canInteract"
        @click="$emit('editConstructorStandingsPrediction')"
      />
    </div>

    <DataTable
      :value="member?.constructorPredictions ?? []"
      :loading="loading"
      striped-rows
      show-gridlines
      size="small"
      data-key="constructor.constructorId.value"
    >
      <template #empty>
        <div class="p-2 text-surface-600 dark:text-surface-300">No constructor predictions found.</div>
      </template>

      <Column header="#" class="w-18">
        <template #body="{ index }">{{ index + 1 }}</template>
      </Column>

      <Column header="Constructor">
        <template #body="{ data }">
          <ConstructorListItem :constructor="data.constructor" :emphasize="false" />
        </template>
      </Column>
    </DataTable>
  </div>
</template>
