<script setup lang="ts">
import RaceListItem from '@/components/RaceListItem.vue'
import type { RaceSummary } from '@/models'
import DataView from 'primevue/dataview'
import { computed } from 'vue'

const props = defineProps<{
  loading: boolean
  races: RaceSummary[]
}>()

defineEmits<{
  (e: 'select', race: RaceSummary): void
}>()

const skeletonCount = 6
const items = computed(() => (props.loading ? Array(skeletonCount).fill(null) : props.races))
</script>

<template>
  <DataView :value="items" layout="grid">
    <template #grid="slotProps">
      <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 p-4">
        <div v-for="(item, index) in slotProps.items" :key="index">
          <RaceListItem :race="item" :loading="loading" emphasize @select="$emit('select', $event)" />
        </div>
      </div>
    </template>

    <template #empty>
      <div class="flex justify-center items-center h-full p-8">No races found.</div>
    </template>
  </DataView>
</template>
