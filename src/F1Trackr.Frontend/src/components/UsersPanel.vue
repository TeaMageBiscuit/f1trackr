<script setup lang="ts">
import type { User } from '@/models'
import Button from 'primevue/button'
import Column from 'primevue/column'
import DataTable from 'primevue/datatable'
import InputText from 'primevue/inputtext'
import Tag from 'primevue/tag'
import { computed, ref } from 'vue'

const props = defineProps<{
  loading: boolean
  users: User[]
}>()

defineEmits<{
  (e: 'create'): void
  (e: 'update', user: User): void
  (e: 'delete', user: User): void
}>()

const globalFilter = ref('')
const rows = computed(() => props.users ?? [])

const adminSeverity = (value?: boolean) => (value ? 'primary' : 'info')
</script>

<template>
  <div>
    <div class="flex flex-col sm:flex-row sm:items-center justify-between gap-3 mb-4">
      <div class="text-surface-600 dark:text-surface-300">Manage Users</div>

      <div class="flex flex-row gap-3">
        <Button icon="pi pi-plus" label="Add User" :disabled="loading" @click="$emit('create')" />

        <div class="w-full sm:w-80">
          <InputText v-model="globalFilter" class="w-full" placeholder="Search users..." :disabled="loading" />
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
        <div class="p-2 text-surface-600 dark:text-surface-300">No users found.</div>
      </template>

      <template #loading>
        <div class="pt-8 text-surface-600 dark:text-surface-300">Loading users...</div>
      </template>

      <Column field="id" header="User Id" class="w-85">
        <template #body="{ data }">
          <code class="uppercase">{{ data.id.value }}</code>
        </template>
      </Column>

      <Column field="name" header="User Name">
        <template #body="{ data }">
          <div class="font-medium">
            {{ data.name }}
          </div>
        </template>
      </Column>

      <Column field="isAdmin" header="Role" class="w-20">
        <template #body="{ data }">
          <Tag :value="data.isAdmin ? 'Admin' : 'User'" :severity="adminSeverity(data.isAdmin)" />
        </template>
      </Column>

      <Column header="Actions" class="w-21">
        <template #body="{ data }">
          <Button
            icon="pi pi-pencil"
            severity="info"
            variant="text"
            rounded
            size="small"
            @click="$emit('update', data)"
          />
          <Button
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
