<script setup lang="ts">
import type { GroupSummary } from '@/models'
import Button from 'primevue/button'
import Column from 'primevue/column'
import DataTable from 'primevue/datatable'
import InputText from 'primevue/inputtext'
import { computed, ref } from 'vue'

const props = defineProps<{
  loading: boolean
  groups: GroupSummary[]
  canManage?: boolean
}>()

defineEmits<{
  (e: 'create'): void
  (e: 'view', group: GroupSummary): void
  (e: 'delete', group: GroupSummary): void
}>()

const globalFilter = ref('')
const rows = computed(() => props.groups ?? [])
</script>

<template>
  <div>
    <div v-if="canManage" class="flex flex-col sm:flex-row sm:items-center justify-between gap-3 mb-4">
      <div class="text-surface-600 dark:text-surface-300">Manage Groups</div>

      <div class="flex flex-row gap-3">
        <Button icon="pi pi-plus" label="Add Group" :disabled="loading" @click="$emit('create')" />

        <div class="w-full sm:w-80">
          <InputText v-model="globalFilter" class="w-full" placeholder="Search groups..." :disabled="loading" />
        </div>
      </div>
    </div>

    <DataTable
      :value="rows"
      :loading="loading"
      :rows="10"
      striped-rows
      show-gridlines
      size="small"
      data-key="name"
      :global-filter-fields="['name']"
      :filters="{ global: { value: globalFilter, matchMode: 'contains' } }"
    >
      <template #empty>
        <div class="p-2 text-surface-600 dark:text-surface-300">No groups found.</div>
      </template>

      <template #loading>
        <div class="pt-8 text-surface-600 dark:text-surface-300">Loading groups...</div>
      </template>

      <Column field="id" header="Group Id" class="w-85">
        <template #body="{ data }">
          <code class="uppercase">{{ data.id.value }}</code>
        </template>
      </Column>

      <Column field="name" header="Group Name">
        <template #body="{ data }">
          <div class="font-medium">
            {{ data.name }}
          </div>
        </template>
      </Column>

      <Column field="season" header="Season">
        <template #body="{ data }">
          {{ data.season }}
        </template>
      </Column>

      <Column header="Actions" class="w-21">
        <template #body="{ data }">
          <Button icon="pi pi-eye" severity="info" variant="text" rounded size="small" @click="$emit('view', data)" />
          <Button
            v-if="canManage"
            icon="pi pi-trash"
            severity="danger"
            variant="text"
            rounded
            size="small"
            @click="$emit('delete', data)"
          />
        </template>
      </Column>
    </DataTable>
  </div>
</template>
