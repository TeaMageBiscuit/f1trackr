<script setup lang="ts">
import UpdateUserModal from '@/modals/UpdateUserModal.vue'
import type { User } from '@/models'
import useHttp from '@/utils/http.ts'
import { computed, inject, type Ref, ref } from 'vue'

const http = useHttp()
const dialogRef = inject<Ref<{ close: (data?: unknown) => void; data?: { user?: User } }>>('dialogRef')
const user = computed(() => dialogRef?.value?.data?.user)

const request = ref<{
  name?: string | null
  accessCode?: string | null
  isAdmin?: boolean | null
}>({
  name: user.value?.name,
  accessCode: null,
  isAdmin: user.value?.isAdmin,
})

const hasErrors = ref(false)
const errors = ref<Record<string, string[]>>({})

async function update() {
  hasErrors.value = false
  errors.value = {}

  const response = await http.put(`/api/admin/users/${user.value?.id.value}`, {
    name: request.value.name ?? '',
    accessCode: request.value.accessCode ?? '',
    isAdmin: request.value.isAdmin ?? false,
  })

  if (response.success) {
    dialogRef?.value.close({ updated: true })
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
  <UpdateUserModal
    v-model:request="request"
    :has-errors="hasErrors"
    :errors="errors"
    @update="update"
    @cancel="cancel"
  />
</template>
