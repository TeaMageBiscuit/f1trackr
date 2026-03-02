<script setup lang="ts">
import GroupsPanel from '@/components/GroupsPanel.vue'
import ImportTools from '@/components/ImportTools.vue'
import UsersPanel from '@/components/UsersPanel.vue'
import type { GroupSummary, RaceId, User } from '@/models'
import Button from 'primevue/button'

defineProps<{
  loading: boolean
  seasons: number[]
  users: User[]
  groups: GroupSummary[]
}>()

defineEmits<{
  (e: 'refresh'): void
  (e: 'import-constructors', season: number): void
  (e: 'import-drivers', season: number): void
  (e: 'import-races', season: number): void
  (e: 'import-race-results', args: RaceId): void
  (e: 'recalculate-scores', args: RaceId): void
  (e: 'createUser'): void
  (e: 'updateUser', user: User): void
  (e: 'deleteUser', user: User): void
  (e: 'createGroup'): void
  (e: 'viewGroup', group: GroupSummary): void
  (e: 'deleteGroup', group: GroupSummary): void
}>()
</script>

<template>
  <div class="p-6 max-w-7xl mx-auto">
    <header class="flex justify-between items-center mb-6">
      <div>
        <h1 class="text-3xl font-bold text-primary">Admin</h1>
        <p class="text-surface-600 dark:text-surface-300 mt-1">Management tools</p>
      </div>

      <div class="flex items-center gap-2">
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
        <ImportTools
          :loading="loading"
          :seasons="seasons"
          @import-constructors="$emit('import-constructors', $event)"
          @import-drivers="$emit('import-drivers', $event)"
          @import-races="$emit('import-races', $event)"
          @import-race-results="$emit('import-race-results', $event)"
          @recalculate-scores="$emit('recalculate-scores', $event)"
        />
      </div>

      <div class="card p-4!">
        <UsersPanel
          :loading="loading"
          :users="users"
          @create="$emit('createUser')"
          @update="$emit('updateUser', $event)"
          @delete="$emit('deleteUser', $event)"
        />
      </div>

      <div class="card p-4!">
        <GroupsPanel
          :loading="loading"
          :groups="groups"
          can-manage
          @create="$emit('createGroup')"
          @view="$emit('viewGroup', $event)"
          @delete="$emit('deleteGroup', $event)"
        />
      </div>
    </div>
  </div>
</template>
