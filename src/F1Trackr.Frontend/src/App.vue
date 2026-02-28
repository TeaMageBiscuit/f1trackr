<script setup lang="ts">
import AppLayout from '@/components/AppLayout.vue'
import type { Account } from '@/models'
import { useCache } from '@/utils/cache.ts'
import useHttp from '@/utils/http.ts'
import { useDark, useToggle } from '@vueuse/core'
import { useDialog } from 'primevue'
import DynamicDialog from 'primevue/dynamicdialog'
import Toast from 'primevue/toast'
import { defineAsyncComponent, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'

const http = useHttp()
const dialog = useDialog()
const router = useRouter()

const bootstrapCache = useCache<boolean>('bootstrap', {
  ttl: 24 * 60 * 60 * 1000,
})

const account = ref<Account>({ isAuthenticated: false })
const canBootstrap = ref(false)
const loginDialog = defineAsyncComponent(() => import('@/dialogs/LoginDialog.vue'))
const bootstrapDialog = defineAsyncComponent(() => import('@/dialogs/BootstrapDialog.vue'))

const darkMode = useDark({
  storageKey: 'f1-trackr-darkmode',
  valueDark: 'f1-dark',
  valueLight: '',
})
const toggleDark = useToggle(darkMode)

onMounted(async () => {
  account.value = await http
    .get<Account>('/api/account/me')
    .then((response) => response.value ?? { isAuthenticated: false })

  canBootstrap.value = await bootstrapCache.get(() =>
    http.get<{ canBootstrap: boolean }>('/api/bootstrap').then((response) => response.value?.canBootstrap ?? false),
  )
})

function navigate(to: string) {
  router.push(to)
}

function showLogin() {
  dialog.open(loginDialog, {
    props: {
      header: 'Login',
      closable: true,
      modal: true,
      contentClass: 'w-120',
    },
  })
}

async function logout() {
  await http.post('/api/account/logout', undefined).then(() => window.location.replace('/'))
}

function showBootstrap() {
  dialog.open(bootstrapDialog, {
    props: {
      header: 'Bootstrap',
      closable: true,
      modal: true,
      contentClass: 'w-120',
    },
  })
}
</script>

<template>
  <DynamicDialog />
  <Toast />

  <AppLayout
    :dark-mode="darkMode"
    :account="account"
    :can-bootstrap="canBootstrap"
    @navigate="navigate"
    @toggle-theme="toggleDark"
    @login="showLogin"
    @logout="logout"
    @bootstrap="showBootstrap"
  >
    <RouterView />
  </AppLayout>
</template>
