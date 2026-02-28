<script setup lang="ts">
import type { GroupMemberSummary, GroupOverview } from '@/models'
import GroupPage from '@/pages/GroupPage.vue'
import useHttp from '@/utils/http'
import { nextTick, onMounted, ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'

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
    const result = await http.get<GroupOverview>(`/api/groups/${groupId}`)
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

const viewMember = (member: GroupMemberSummary) => {
  router.push({ name: 'group-member', params: { groupId: member.groupId.value, userId: member.userId.value } })
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
  <GroupPage :loading="loading" :group="group" @refresh="refresh" @back="goBack" @view-member="viewMember" />
</template>
