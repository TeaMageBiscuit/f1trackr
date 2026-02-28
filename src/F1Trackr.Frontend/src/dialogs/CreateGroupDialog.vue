<script setup lang="ts">
import CreateGroupModal from '@/modals/CreateGroupModal.vue'
import useHttp from '@/utils/http.ts'
import { inject, onMounted, type Ref, ref } from 'vue'

const http = useHttp()
const dialogRef = inject<Ref<{ close: (data?: unknown) => void }>>('dialogRef')

const request = ref<{
  name?: string | null
  season?: number | null
}>({})

const seasons = ref<number[]>([])
const hasErrors = ref(false)
const errors = ref<Record<string, string[]>>({})

async function create() {
  hasErrors.value = false
  errors.value = {}

  const response = await http.post('/api/admin/groups', {
    name: request.value.name ?? '',
    season: request.value.season,
  })

  if (response.success) {
    dialogRef?.value.close({ created: true })
    return
  }

  hasErrors.value = true
  errors.value = response.errors!
}

function cancel() {
  dialogRef?.value.close()
}

onMounted(async () => {
  const currentSeason = new Date().getFullYear()
  seasons.value = await http.get<number[]>('/api/seasons').then((response) => response.value ?? [currentSeason])
})
</script>

<template>
  <CreateGroupModal
    v-model:request="request"
    :seasons="seasons"
    :has-errors="hasErrors"
    :errors="errors"
    @create="create"
    @cancel="cancel"
  />
</template>
