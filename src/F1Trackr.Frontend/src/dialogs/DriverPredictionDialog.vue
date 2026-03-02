<script setup lang="ts">
import DriverPredictionModal from '@/modals/DriverPredictionModal.vue'
import type { DriverSummary } from '@/models'
import useHttp from '@/utils/http.ts'
import { computed, inject, onMounted, ref, type Ref } from 'vue'

interface DialogData {
  endpoint: string
  season: number
  drivers: DriverSummary[]
}

interface DialogRef {
  close: (data?: unknown) => void
  data: DialogData
}

const http = useHttp()
const dialogRef = inject<Ref<DialogRef>>('dialogRef')

const loading = ref<boolean>(false)
const drivers = ref<DriverSummary[]>([])
const hasErrors = ref(false)
const errors = ref<Record<string, string[]>>({})
const dialogData = computed(() => dialogRef?.value.data)

onMounted(async () => {
  loading.value = true

  try {
    drivers.value = dialogData.value?.drivers ?? []
  } finally {
    loading.value = false
  }
})

async function onSave(positions: Record<string, number>) {
  loading.value = true
  hasErrors.value = false
  errors.value = {}

  try {
    const response = await http.put(dialogData.value!.endpoint, {
      positions: positions,
    })

    if (response.success) {
      dialogRef?.value.close({ added: true })
      return
    }

    hasErrors.value = true
    errors.value = response.errors!
  } finally {
    loading.value = false
  }
}

function onCancel() {
  dialogRef?.value.close()
}
</script>

<template>
  <DriverPredictionModal
    v-model="drivers"
    :loading="loading"
    :has-errors="hasErrors"
    :errors="errors"
    @save="onSave"
    @cancel="onCancel"
  />
</template>
