<script setup lang="ts">
import type { GroupMember } from '@/models'
import Column from 'primevue/column'
import DataTable from 'primevue/datatable'

defineProps<{
  loading: boolean
  member?: GroupMember
}>()
</script>

<template>
  <DataTable
    :value="member?.scoreSnapshots ?? []"
    :loading="loading"
    :rows="10"
    striped-rows
    show-gridlines
    size="small"
  >
    <template #empty>
      <div class="p-2 text-surface-600 dark:text-surface-300">
        <span v-if="member">No score snapshots found.</span>
        <span v-else>Loading member...</span>
      </div>
    </template>

    <Column header="Race">
      <template #body="{ data }">
        <span>{{ data.raceId?.season }} · Round {{ data.raceId?.round }}</span>
      </template>
    </Column>

    <Column field="basePoints" header="Base" />
    <Column field="constructorStandingsPenalty" header="Constructor" />
    <Column field="driverStandingsPenalty" header="Driver" />
    <Column field="raceBonusThisRound" header="Race Bonus (Round)" />
    <Column field="raceBonusTotal" header="Race Bonus (Total)" />
    <Column field="totalPoints" header="Total" />
    <Column header="Computed At">
      <template #body="{ data }">
        {{ data.computedAt.toLocaleString() }}
      </template>
    </Column>
  </DataTable>
</template>
