<script setup lang="ts">
import type { RaceSummary } from '@/models'
import Skeleton from 'primevue/skeleton'

defineProps<{
  race?: RaceSummary
  loading?: boolean
  emphasize?: boolean
}>()

defineEmits<{
  (e: 'select', race: RaceSummary): void
}>()

const formatDate = (dateStr?: string | Date | null) => {
  if (!dateStr) return ''

  return new Date(dateStr).toLocaleDateString('en-GB', {
    day: 'numeric',
    month: 'short',
    year: 'numeric',
  })
}
</script>

<template>
  <div
    class="flex flex-col p-4 border border-surface-200 dark:border-surface-700 bg-surface-0 dark:bg-surface-900 rounded-lg shadow-sm hover:shadow-md transition-shadow group"
    :class="{ 'cursor-pointer hover:border-primary!': !loading && emphasize, 'cursor-not-allowed': loading }"
    @click="race ? $emit('select', race) : void 0"
  >
    <template v-if="loading">
      <div class="flex justify-between items-start mb-2">
        <Skeleton width="6rem" height="0.875rem" />
        <Skeleton width="5rem" height="0.875rem" />
      </div>

      <Skeleton width="75%" height="1.5rem" class="mb-3" />

      <div class="mt-auto">
        <div class="flex items-center gap-2">
          <Skeleton shape="circle" width="14px" height="14px" />
          <Skeleton width="10rem" height="1rem" />
        </div>
        <Skeleton width="12rem" height="0.875rem" class="mt-2" />
      </div>
    </template>

    <template v-else>
      <div class="flex justify-between items-start mb-2">
        <span class="text-xs font-bold text-primary uppercase tracking-wider">Round {{ race?.id.round }}</span>
        <span class="text-xs text-surface-500 font-mono">{{ formatDate(race?.grandPrixTime) }}</span>
      </div>
      <h3
        class="text-xl font-bold mb-1 line-clamp-1 transition-colors"
        :class="{ 'group-hover:text-primary': emphasize }"
      >
        {{ race?.name }}
      </h3>
      <div class="mt-auto">
        <div class="flex items-center gap-1 text-sm text-surface-600 dark:text-surface-400">
          <i class="pi pi-map-marker text-xs"></i>
          <span class="line-clamp-1">{{ race?.circuit }}</span>
        </div>
        <div class="text-xs text-surface-400 mt-1">
          {{ race?.location }},
          {{ race?.country }}
        </div>
      </div>
    </template>
  </div>
</template>
