<script setup lang="ts">
import type { GroupMemberSummary, GroupOverview } from '@/models'
import Button from 'primevue/button'
import Column from 'primevue/column'
import DataTable from 'primevue/datatable'
import Skeleton from 'primevue/skeleton'

defineProps<{
  loading: boolean
  group?: GroupOverview
}>()

defineEmits<{
  (e: 'refresh'): void
  (e: 'back'): void
  (e: 'addMember'): void
  (e: 'viewMember', member: GroupMemberSummary): void
  (e: 'removeMember', member: GroupMemberSummary): void
}>()
</script>

<template>
  <div class="p-6 max-w-7xl mx-auto">
    <header class="flex justify-between items-center mb-6">
      <div>
        <h1 class="text-3xl font-bold text-primary">Group</h1>
        <p class="text-surface-600 dark:text-surface-300 mt-1">
          <Skeleton v-if="loading" height="1.55rem" width="10rem" />
          <span v-else-if="group">{{ group.name }} · {{ group.season }}</span>
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
        <div class="flex flex-col gap-1">
          <div class="text-surface-600 dark:text-surface-300">Group Id</div>
          <div>
            <Skeleton v-if="loading" height="1.55rem" width="20rem" />
            <code v-else-if="group" class="uppercase">{{ group.id.value }}</code>
            <span v-else class="text-surface-600 dark:text-surface-300">—</span>
          </div>
        </div>
      </div>

      <div class="card p-4!">
        <div class="flex justify-between items-center mb-3">
          <div class="font-medium">Members</div>

          <div class="flex items-center gap-3">
            <div class="text-surface-600 dark:text-surface-300">
              <span>{{ group?.members.length ?? 0 }} total</span>
            </div>

            <Button
              icon="pi pi-user-plus"
              label="Add Member"
              :disabled="loading || !group"
              @click="$emit('addMember')"
            />
          </div>
        </div>

        <DataTable
          :value="group?.members ?? []"
          :loading="loading"
          :rows="10"
          striped-rows
          show-gridlines
          size="small"
          data-key="userId"
        >
          <template #empty>
            <div class="p-2 text-surface-600 dark:text-surface-300">
              <span v-if="group">No members found.</span>
              <span v-else>Loading group...</span>
            </div>
          </template>

          <template #loading>
            <div class="pt-8 text-surface-600 dark:text-surface-300">Loading group...</div>
          </template>

          <Column field="userId" header="User Id" class="w-85">
            <template #body="{ data }">
              <code class="uppercase">{{ data.userId.value }}</code>
            </template>
          </Column>

          <Column field="userName" header="Name">
            <template #body="{ data }">
              <div class="font-medium">
                {{ data.userName }}
              </div>
            </template>
          </Column>

          <Column field="currentScore" header="Current Score">
            <template #body="{ data }">
              {{ data.currentScore }}
            </template>
          </Column>

          <Column header="Actions" class="w-21">
            <template #body="{ data }">
              <Button
                icon="pi pi-eye"
                severity="info"
                variant="text"
                rounded
                size="small"
                :disabled="loading"
                @click="$emit('viewMember', data)"
              />
              <Button
                icon="pi pi-trash"
                severity="danger"
                variant="text"
                rounded
                size="small"
                :disabled="loading"
                @click="$emit('removeMember', data)"
              />
            </template>
          </Column>
        </DataTable>
      </div>
    </div>
  </div>
</template>
