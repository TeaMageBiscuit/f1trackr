<script setup lang="ts">
import DriverListItem from '@/components/DriverListItem.vue'
import type { DriverSummary } from '@/models'
import Button from 'primevue/button'
import PickList from 'primevue/picklist'
import { computed, ref } from 'vue'

const model = defineModel<[DriverSummary[], DriverSummary[]]>()

defineProps<{
  loading: boolean
  hasErrors: boolean
  errors: Record<string, string[]>
}>()

const emits = defineEmits<{
  (e: 'save', positions: Record<string, number>): void
  (e: 'cancel'): void
}>()

const selected = ref<[DriverSummary[], DriverSummary[]]>([[], []])

const canSubmit = computed(() => model.value && model.value[1].length === 10)

const submitPrediction = () => {
  const positions: Record<string, number> = {}
  model.value![1].forEach((d, index) => {
    positions[d.id.value] = index + 1
  })
  emits('save', positions)
}
</script>

<template>
  <div>
    <PickList
      v-if="model"
      v-model="model"
      v-model:selection="selected"
      data-key="id"
      scroll-height="30rem"
      :show-source-controls="false"
      breakpoint="0px"
      meta-key-selection
      :move-to-target-props="{
        disabled: model[1].length >= 10 || model[1].length + selected[0].length > 10,
      }"
      :move-all-to-target-props="{
        hidden: true,
      }"
    >
      <template #sourceheader>
        <span class="font-semibold">Available Drivers</span>
      </template>

      <template #targetheader>
        <span class="font-semibold">Predicted Top 10</span>
      </template>

      <template #option="{ option }">
        <div class="flex items-center p-2 w-full">
          <span v-if="model[1].includes(option)" class="font-bold mr-4 w-8 text-secondary">
            {{ model[1].indexOf(option) + 1 }}.
          </span>
          <DriverListItem :driver="option" :emphasize="false" />
        </div>
      </template>
    </PickList>

    <div class="flex justify-center gap-2 mt-4">
      <Button label="Save Prediction" class="w-1/3" :disabled="loading || !canSubmit" @click="submitPrediction" />
      <Button label="Cancel" severity="secondary" outlined class="w-1/3" :disabled="loading" @click="$emit('cancel')" />
    </div>
  </div>
</template>
