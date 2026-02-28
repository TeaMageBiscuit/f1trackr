<script setup lang="ts">
import CreateGroupDialog from '@/dialogs/CreateGroupDialog.vue'
import type { GroupSummary, RaceId, User } from '@/models'
import AdminPage from '@/pages/AdminPage.vue'
import { useCache } from '@/utils/cache.ts'
import useHttp from '@/utils/http'
import { useDialog, useToast } from 'primevue'
import { defineAsyncComponent, nextTick, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'

const dialog = useDialog()
const toast = useToast()
const http = useHttp()
const router = useRouter()
const CreateUserDialog = defineAsyncComponent(() => import('@/dialogs/CreateUserDialog.vue'))
const UpdateUserDialog = defineAsyncComponent(() => import('@/dialogs/UpdateUserDialog.vue'))

const seasonsCache = useCache<number[]>('seasons', {
  ttl: 60 * 60 * 1000,
  storage: sessionStorage,
  initialValue: undefined,
})

const loading = ref(false)
const importSeason = ref(new Date().getFullYear())
const importSeasons = ref<number[]>([])
const importRound = ref(1)
const users = ref<User[]>([])
const groups = ref<GroupSummary[]>([])

const refresh = async () => {
  loading.value = true

  await nextTick()

  try {
    const season = importSeason.value
    importSeasons.value = await seasonsCache.get(() =>
      http.get<number[]>('/api/seasons').then((response) => response.value ?? [season]),
    )

    const usersResult = await http.get<User[]>('/api/admin/users')
    if (usersResult.success) {
      users.value = usersResult.value!
    }

    const groupsResult = await http.get<GroupSummary[]>('/api/admin/groups')
    if (groupsResult.success) {
      groups.value = groupsResult.value!
    }
  } finally {
    loading.value = false
  }
}

async function importConstructors(season: number) {
  loading.value = true

  try {
    const result = await http.put(`/api/admin/constructors/${season}`)

    if (result.success) {
      toast.add({
        severity: 'success',
        summary: 'Constructors Imported',
        detail: 'Constructors were successfully imported.',
        life: 3000,
      })
    } else {
      toast.add({
        severity: 'error',
        summary: 'Failed to import constructors',
        detail: `${Object.values(result.errors!).flat().join(' ')}`,
      })
    }
  } finally {
    loading.value = false
  }
}

async function importDrivers(season: number) {
  loading.value = true

  try {
    const result = await http.put(`/api/admin/drivers/${season}`)

    if (result.success) {
      toast.add({
        severity: 'success',
        summary: 'Drivers Imported',
        detail: 'Drivers were successfully imported.',
        life: 3000,
      })
    } else {
      toast.add({
        severity: 'error',
        summary: 'Failed to import drivers',
        detail: `${Object.values(result.errors!).flat().join(' ')}`,
      })
    }
  } finally {
    loading.value = false
  }
}

async function importRaces(season: number) {
  loading.value = true

  try {
    const result = await http.put(`/api/admin/races/${season}`)

    if (result.success) {
      toast.add({
        severity: 'success',
        summary: 'Races Imported',
        detail: 'Races were successfully imported.',
        life: 3000,
      })
    } else {
      toast.add({
        severity: 'error',
        summary: 'Failed to import races',
        detail: `${Object.values(result.errors!).flat().join(' ')}`,
      })
    }
  } finally {
    loading.value = false
  }
}

async function importResults({ season, round }: RaceId) {
  loading.value = true

  try {
    const result = await http.put(`/api/admin/results/${season}/${round}`)

    if (result.success) {
      toast.add({
        severity: 'success',
        summary: 'Results Imported',
        detail: 'Results were successfully imported.',
        life: 3000,
      })
    } else {
      toast.add({
        severity: 'error',
        summary: 'Failed to import race results',
        detail: `${Object.values(result.errors!).flat().join(' ')}`,
      })
    }
  } finally {
    loading.value = false
  }
}

async function recalculateScores({ season, round }: RaceId) {
  loading.value = true

  try {
    const result = await http.put(`/api/admin/scores/${season}/${round}`)

    if (result.success) {
      toast.add({
        severity: 'success',
        summary: 'Scores Recalculated',
        detail: 'Scores were successfully recalculated.',
        life: 3000,
      })
    } else {
      toast.add({
        severity: 'error',
        summary: 'Failed to recalculate scores',
        detail: `${Object.values(result.errors!).flat().join(' ')}`,
      })
    }
  } finally {
    loading.value = false
  }
}

function showCreateUserDialog() {
  dialog.open(CreateUserDialog, {
    props: {
      header: 'New User',
      closable: true,
      modal: true,
      contentClass: 'w-120',
    },
    onClose: (options) => {
      const data = options?.data as { created?: boolean } | undefined
      if (data?.created) {
        toast.add({
          severity: 'success',
          summary: 'User Created',
          detail: 'The user was successfully created.',
          life: 3000,
        })
        refresh()
      }
    },
  })
}

function showUpdateUserDialog(user: User) {
  dialog.open(UpdateUserDialog, {
    props: {
      header: 'Update User',
      closable: true,
      modal: true,
      contentClass: 'w-120',
    },
    data: {
      user,
    },
    onClose: (options) => {
      const data = options?.data as { updated?: boolean } | undefined
      if (data?.updated) {
        toast.add({
          severity: 'success',
          summary: 'User Updated',
          detail: 'The user was successfully updated.',
          life: 3000,
        })
        refresh()
      }
    },
  })
}

async function deleteUser(user: User) {
  const result = await http.del(`/api/admin/users/${user.id.value}`)

  if (result.success) {
    await refresh()
  } else {
    toast.add({
      severity: 'error',
      summary: 'Failed to delete user',
      detail: `${Object.values(result.errors!).flat().join(' ')}`,
      life: 3000,
    })
  }
}

function showCreateGroupDialog() {
  dialog.open(CreateGroupDialog, {
    props: {
      header: 'New Group',
      closable: true,
      modal: true,
      contentClass: 'w-120',
    },
    onClose: (options) => {
      const data = options?.data as { created?: boolean } | undefined
      if (data?.created) {
        toast.add({
          severity: 'success',
          summary: 'Group Created',
          detail: 'The group was successfully created.',
          life: 3000,
        })
        refresh()
      }
    },
  })
}

function viewGroup(group: GroupSummary) {
  router.push({ name: 'admin-group', params: { groupId: group.id.value } })
}

async function deleteGroup(group: GroupSummary) {
  const result = await http.del(`/api/admin/groups/${group.id.value}`)

  if (result.success) {
    await refresh()
  } else {
    toast.add({
      severity: 'error',
      summary: 'Failed to delete group',
      detail: `${Object.values(result.errors!).flat().join(' ')}`,
      life: 3000,
    })
  }
}

onMounted(refresh)
</script>

<template>
  <AdminPage
    v-model:import-season="importSeason"
    v-model:import-round="importRound"
    :loading="loading"
    :seasons="importSeasons"
    :users="users"
    :groups="groups"
    @refresh="refresh"
    @import-constructors="importConstructors"
    @import-drivers="importDrivers"
    @import-races="importRaces"
    @import-results="importResults"
    @recalculate-scores="recalculateScores"
    @create-user="showCreateUserDialog"
    @update-user="showUpdateUserDialog"
    @delete-user="deleteUser"
    @create-group="showCreateGroupDialog"
    @view-group="viewGroup"
    @delete-group="deleteGroup"
  />
</template>
