<script setup lang="ts">
import type { ConstructorSummary } from '@/models'
import getFlagUrl from '@/utils/nationalityFlag'
import Skeleton from 'primevue/skeleton'

const { emphasize = true } = defineProps<{
  constructor?: ConstructorSummary
  loading?: boolean
  emphasize?: boolean
}>()
</script>

<template>
  <div class="flex items-center gap-2">
    <template v-if="loading">
      <Skeleton shape="rectangle" width="24px" height="16px" class="rounded-sm" />
      <Skeleton width="10rem" height="28px" />
    </template>

    <template v-else>
      <img
        :src="getFlagUrl(constructor?.nationality)"
        :alt="constructor?.nationality ?? 'unknown nationality'"
        class="w-6 h-4 object-cover rounded-sm shadow-sm"
      />
      <span :class="{ 'font-semibold text-lg group-hover:text-primary': emphasize }">
        {{ constructor?.name }}
      </span>
    </template>
  </div>
</template>
