<script setup lang="ts">
import AddUserToGroupDialog from '@/dialogs/AddUserToGroupDialog.vue'
import type { GroupMemberSummary, GroupOverview } from '@/models'
import AdminGroupPage from '@/pages/AdminGroupPage.vue'
import useHttp from '@/utils/http'
import { useDialog, useToast } from 'primevue'
import { nextTick, onMounted, ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const dialog = useDialog()
const toast = useToast()
const http = useHttp()
const route = useRoute()
const router = useRouter()

const loading = ref(false)
const group = ref<GroupOverview | undefined>(undefined)

const refresh = async () => {
  const groupId = String(route.params.groupId ?? '').trim()
  if (!groupId) {
    group.value = undefined
    return
  }

  loading.value = true
  await nextTick()

  try {
    const result = await http.get<GroupOverview>(`/api/admin/groups/${groupId}`)
    if (result.success) {
      group.value = result.value!
    } else {
      group.value = undefined
    }
  } finally {
    loading.value = false
  }
}

const goBack = () => {
  router.back()
}

function showAddUserToGroupDialog() {
  dialog.open(AddUserToGroupDialog, {
    props: {
      header: 'Add User to Group',
      closable: true,
      modal: true,
      contentClass: 'w-120',
    },
    data: {
      groupId: route.params.groupId,
    },
    onClose: (options) => {
      const data = options?.data as { added?: boolean } | undefined
      if (data?.added) {
        toast.add({
          severity: 'success',
          summary: 'User Added to Group',
          detail: 'The user was successfully added to the group.',
          life: 3000,
        })
        refresh()
      }
    },
  })
}

async function removeMember(member: GroupMemberSummary) {
  const result = await http.del(`/api/admin/groups/${route.params.groupId}/members/${member.userId.value}`)

  if (result.success) {
    await refresh()
  } else {
    toast.add({
      severity: 'error',
      summary: 'Failed to remove user',
      detail: `${Object.values(result.errors!).flat().join(' ')}`,
      life: 3000,
    })
  }
}

const viewMember = (member: GroupMemberSummary) => {
  router.push({ name: 'admin-group-member', params: { groupId: member.groupId.value, userId: member.userId.value } })
}

onMounted(refresh)

watch(
  () => route.params.groupId,
  async () => {
    await refresh()
  },
)
</script>

<template>
  <AdminGroupPage
    :loading="loading"
    :group="group"
    @refresh="refresh"
    @back="goBack"
    @add-member="showAddUserToGroupDialog"
    @remove-member="removeMember"
    @view-member="viewMember"
  />
</template>
