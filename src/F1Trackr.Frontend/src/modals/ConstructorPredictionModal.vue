<script setup lang="ts">
import ConstructorListItem from '@/components/ConstructorListItem.vue'
import type { ConstructorSummary } from '@/models'
import Button from 'primevue/button'
import Message from 'primevue/message'
import OrderList from 'primevue/orderlist'

const model = defineModel<ConstructorSummary[]>()

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
  model.value?.forEach((c, index) => {
    positions[c.id.value] = index + 1
  })
  emits('save', positions)
}
</script>

<template>
  <div>
    <Message v-if="hasErrors" class="mt-1 mb-4" severity="error">
      <div>Failed to save predictions.</div>

      <ul class="list-disc pl-5">
        <li v-for="(line, index) in errors['']" :key="index">
          {{ line }}
        </li>
      </ul>
    </Message>

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
          <ConstructorListItem :constructor="option" :emphasize="false" />
        </div>
      </template>
    </OrderList>

    <div class="flex justify-center gap-2 mt-4">
      <Button label="Save Prediction" class="w-1/3" :disabled="loading" @click="submitPrediction" />
      <Button label="Cancel" severity="secondary" outlined class="w-1/3" :disabled="loading" @click="$emit('cancel')" />
    </div>
  </div>
</template>
