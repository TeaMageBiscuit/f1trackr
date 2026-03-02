<script setup lang="ts">
import DriverListItem from '@/components/DriverListItem.vue'
import type { DriverSummary } from '@/models'
import DataView from 'primevue/dataview'
import { computed } from 'vue'

const props = defineProps<{
  loading: boolean
  drivers: DriverSummary[]
}>()

const skeletonCount = 10
const items = computed(() => (props.loading ? Array(skeletonCount).fill(null) : props.drivers))
</script>

<template>
  <DataView :value="items" layout="grid">
    <template #grid="slotProps">
      <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5 gap-4 p-4">
        <div
          v-for="(item, index) in slotProps.items"
          :key="index"
          class="p-4 border border-surface-200 dark:border-surface-700 bg-surface-0 dark:bg-surface-900 rounded-lg shadow-sm hover:shadow-md transition-shadow group"
          :class="{ 'cursor-pointer hover:border-primary!': !loading, 'cursor-not-allowed': loading }"
        >
          <DriverListItem :driver="item" :loading="loading" />
        </div>
      </div>
    </template>

    <template #empty>
      <div class="flex justify-center items-center h-full p-8">No drivers found.</div>
    </template>
  </DataView>
</template>
