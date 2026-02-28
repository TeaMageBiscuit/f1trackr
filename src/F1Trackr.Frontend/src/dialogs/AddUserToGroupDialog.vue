<script setup lang="ts">
import AddUserToGroupModal from '@/modals/AddUserToGroupModal.vue'
import type { User } from '@/models'
import useHttp from '@/utils/http.ts'
import { computed, inject, onMounted, type Ref, ref } from 'vue'

type DialogRefShape = {
  close: (data?: unknown) => void
  data?: unknown
}

const http = useHttp()
const dialogRef = inject<Ref<DialogRefShape>>('dialogRef')

const groupId = computed(() => {
  const data = dialogRef?.value?.data as { groupId?: string } | undefined
  return String(data?.groupId ?? '').trim()
})

const request = ref<{
  user?: User | null
}>({})

const users = ref<User[]>([])
const loadingUsers = ref(false)

const hasErrors = ref(false)
const errors = ref<Record<string, string[]>>({})

onMounted(async () => {
  loadingUsers.value = true
  try {
    const result = await http.get<User[]>('/api/admin/users')
    if (result.success) {
      users.value = result.value ?? []
    } else {
      users.value = []
    }
  } finally {
    loadingUsers.value = false
  }
})

async function add() {
  hasErrors.value = false
  errors.value = {}

  if (!groupId.value) {
    hasErrors.value = true
    errors.value = { '': ['Missing group id.'] }
    return
  }

  const userId = request.value.user?.id.value
  if (!userId) {
    hasErrors.value = true
    errors.value = { UserId: ['Please select a user.'] }
    return
  }

  const response = await http.post(`/api/admin/groups/${groupId.value}/members`, {
    userId,
  })

  if (response.success) {
    dialogRef?.value.close({ added: true })
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
  <AddUserToGroupModal
    v-model:request="request"
    :users="users"
    :loading-users="loadingUsers"
    :has-errors="hasErrors"
    :errors="errors"
    @add="add"
    @cancel="cancel"
  />
</template>
