<script setup lang="ts">
import type { Account } from '@/models'
import Button from 'primevue/button'
import Menubar from 'primevue/menubar'
import { computed } from 'vue'

const props = defineProps<{
  darkMode: boolean
  account: Account
  canBootstrap: boolean
}>()

const emit = defineEmits<{
  (e: 'navigate', to: string): void
  (e: 'toggleTheme'): void
  (e: 'login'): void
  (e: 'logout'): void
  (e: 'bootstrap'): void
}>()

const model = computed(() => {
  const items = [
    {
      label: 'Home',
      command: () => emit('navigate', '/'),
    },
  ]

  if (props.account.isAdmin) {
    items.push({
      label: 'Admin',
      command: () => emit('navigate', '/admin'),
    })
  }

  if (props.account.isAuthenticated) {
    items.push({
      label: 'Groups',
      command: () => emit('navigate', '/groups'),
    })
  }

  return items
})
</script>

<template>
  <div class="min-h-screen flex flex-col">
    <header class="w-full border-b border-surface-200 dark:border-surface-700 bg-surface-0 dark:bg-surface-900">
      <div class="mx-auto w-full max-w-6xl">
        <Menubar :model="model" class="w-full border-0 rounded-none bg-transparent px-0">
          <template #start>
            <img src="/f1-logo.svg" alt="F1 Trackr Logo" width="96" height="24" />
          </template>

          <template #end>
            <div class="flex items-center gap-2">
              <Button
                v-if="account.isAuthenticated"
                icon="pi pi-sign-out"
                label="Logout"
                severity="secondary"
                outlined
                size="small"
                @click="$emit('logout')"
              />
              <Button
                v-if="!account.isAuthenticated"
                icon="pi pi-sign-in"
                label="Login"
                severity="secondary"
                outlined
                size="small"
                @click="$emit('login')"
              />
              <Button
                v-if="canBootstrap"
                icon="pi pi-wrench"
                label="Bootstrap"
                severity="secondary"
                outlined
                size="small"
                @click="$emit('bootstrap')"
              />
              <Button
                :icon="darkMode ? 'pi pi-moon' : 'pi pi-sun'"
                severity="secondary"
                outlined
                size="small"
                @click="$emit('toggleTheme')"
              />
            </div>
          </template>
        </Menubar>
      </div>
    </header>

    <main class="flex-1 p-4">
      <slot />
    </main>
  </div>
</template>
