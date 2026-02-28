<script setup lang="ts">
import CreateUserModal from '@/modals/CreateUserModal.vue'
import useHttp from '@/utils/http.ts'
import { inject, type Ref, ref } from 'vue'

const http = useHttp()
const dialogRef = inject<Ref<{ close: (data?: unknown) => void }>>('dialogRef')

const request = ref<{
  name?: string | null
  accessCode?: string | null
  isAdmin?: boolean | null
}>({})

const hasErrors = ref(false)
const errors = ref<Record<string, string[]>>({})

async function create() {
  hasErrors.value = false
  errors.value = {}

  const response = await http.post('/api/admin/users', {
    name: request.value.name ?? '',
    accessCode: request.value.accessCode ?? '',
    isAdmin: request.value.isAdmin ?? false,
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
</script>

<template>
  <CreateUserModal
    v-model:request="request"
    :has-errors="hasErrors"
    :errors="errors"
    @create="create"
    @cancel="cancel"
  />
</template>
