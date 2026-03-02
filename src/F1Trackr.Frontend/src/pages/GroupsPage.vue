<script setup lang="ts">
import GroupsPanel from '@/components/GroupsPanel.vue'
import type { GroupSummary, User } from '@/models'
import Button from 'primevue/button'

defineProps<{
  loading: boolean
  users: User[]
  groups: GroupSummary[]
}>()

defineEmits<{
  (e: 'refresh'): void
  (e: 'viewGroup', group: GroupSummary): void
}>()
</script>

<template>
  <div class="p-6 max-w-7xl mx-auto">
    <header class="flex justify-between items-center mb-6">
      <div>
        <h1 class="text-3xl font-bold text-primary">My Groups</h1>
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
        <GroupsPanel :loading="loading" :groups="groups" @view="$emit('viewGroup', $event)" />
      </div>
    </div>
  </div>
</template>
