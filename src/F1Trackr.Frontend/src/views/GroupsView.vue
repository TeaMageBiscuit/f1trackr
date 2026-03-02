<script setup lang="ts">
import type { GroupSummary, User } from '@/models'
import GroupsPage from '@/pages/GroupsPage.vue'
import useHttp from '@/utils/http'
import { nextTick, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'

const http = useHttp()
const router = useRouter()

const loading = ref(false)
const users = ref<User[]>([])
const groups = ref<GroupSummary[]>([])

const refresh = async () => {
  loading.value = true

  await nextTick()

  try {
    const groupsResult = await http.get<GroupSummary[]>('/api/groups')
    if (groupsResult.success) {
      groups.value = groupsResult.value!
    }
  } finally {
    loading.value = false
  }
}

function viewGroup(group: GroupSummary) {
  router.push({ name: 'group', params: { groupId: group.id.value } })
}

onMounted(refresh)
</script>

<template>
  <GroupsPage :loading="loading" :users="users" :groups="groups" @refresh="refresh" @view-group="viewGroup" />
</template>
