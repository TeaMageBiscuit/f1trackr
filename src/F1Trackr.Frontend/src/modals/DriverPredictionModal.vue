<script setup lang="ts">
import DriverListItem from '@/components/DriverListItem.vue'
import type { DriverSummary } from '@/models'
import Button from 'primevue/button'
import OrderList from 'primevue/orderlist'

const model = defineModel<DriverSummary[]>()

defineProps<{
  loading: boolean
  hasErrors: boolean
  errors: Record<string, string[]>
}>()

const emits = defineEmits<{
  (e: 'save', positions: Record<string, number>): void
  (e: 'cancel'): void
}>()

const submitPrediction = () => {
  const positions: Record<string, number> = {}
  model.value?.forEach((d, index) => {
    positions[d.id.value] = index + 1
  })
  emits('save', positions)
}
</script>

<template>
  <div>
    <OrderList
      v-model="model"
      data-key="id"
      :pt="{
        root: 'flex-col',
        controls: 'flex-row',
        pcListbox: { root: 'w-full' },
      }"
      scroll-height="30rem"
      meta-key-selection
    >
      <template #option="{ option, index }">
        <div class="flex items-center p-2 w-full">
          <span class="font-bold mr-4 w-8 text-secondary">{{ index + 1 }}.</span>
          <DriverListItem :driver="option" :emphasize="false" />
        </div>
      </template>
    </OrderList>

    <div class="flex justify-center gap-2 mt-4">
      <Button label="Save Prediction" class="w-1/3" :disabled="loading" @click="submitPrediction" />
      <Button label="Cancel" severity="secondary" outlined class="w-1/3" :disabled="loading" @click="$emit('cancel')" />
    </div>
  </div>
</template>
