<script setup lang="ts">
import type { DriverSummary } from '@/models'
import getFlagUrl from '@/utils/nationalityFlag.ts'
import Skeleton from 'primevue/skeleton'

const { driver = undefined, emphasize = true } = defineProps<{
  driver?: DriverSummary
  loading?: boolean
  emphasize?: boolean
}>()
</script>

<template>
  <div>
    <template v-if="loading">
      <Skeleton width="12rem" height="1.125rem" class="mb-2" />
      <div class="flex items-center gap-2">
        <Skeleton shape="rectangle" width="16px" height="12px" class="rounded-sm" />
        <Skeleton width="3rem" height="0.875rem" />
      </div>
    </template>

    <template v-else>
      <template v-if="emphasize">
        <span class="font-medium group-hover:text-primary"> {{ driver?.givenName }} {{ driver?.familyName }} </span>
        <div class="flex items-center gap-2">
          <img
            :src="getFlagUrl(driver?.nationality)"
            :alt="driver?.nationality ?? 'unknown nationality'"
            class="w-4 h-3 object-cover rounded-sm shadow-sm"
          />
          <span class="text-xs font-mono text-surface-500 uppercase">{{ driver?.code }}</span>
        </div>
      </template>

      <template v-else>
        <div class="flex items-center gap-2">
          <img
            :src="getFlagUrl(driver?.nationality)"
            :alt="driver?.nationality ?? 'unknown nationality'"
            class="w-4 h-3 object-cover rounded-sm shadow-sm"
          />
          <span class="text-xs font-mono text-surface-500 uppercase">{{ driver?.code }}</span>
          <span class="font-medium group-hover:text-primary"> {{ driver?.givenName }} {{ driver?.familyName }}</span>
        </div>
      </template>
    </template>
  </div>
</template>
