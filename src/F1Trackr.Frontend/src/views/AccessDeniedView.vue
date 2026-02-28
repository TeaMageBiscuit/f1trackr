<script setup lang="ts">
import Button from 'primevue/button'
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const router = useRouter()
const route = useRoute()

const attemptedRoute = computed(() => {
  const raw = route.query.from
  if (typeof raw !== 'string' || raw.trim().length === 0) return null

  try {
    return decodeURIComponent(raw)
  } catch {
    return raw
  }
})

function goHome() {
  router.push({ name: 'home' })
}
</script>

<template>
  <div class="mx-auto w-full max-w-6xl">
    <div class="flex min-h-[60vh] items-center justify-center">
      <div class="card w-full max-w-xl p-10!">
        <div class="flex items-start gap-3">
          <i class="pi pi-lock text-3xl text-surface-500" />
          <div class="flex-1">
            <div class="text-xl font-semibold">Access denied</div>
            <p class="mt-1 text-surface-600 dark:text-surface-300">You don’t have permission to view this page.</p>

            <p v-if="attemptedRoute" class="mt-3 text-sm text-surface-600 dark:text-surface-300">
              Attempted route:
              <code
                class="ml-1 rounded bg-surface-100 px-2 py-1 text-surface-900 dark:bg-surface-800 dark:text-surface-0"
              >
                {{ attemptedRoute }}
              </code>
            </p>
          </div>
        </div>

        <div class="mt-4 flex flex-wrap gap-2">
          <Button icon="pi pi-home" label="Go to Home" @click="goHome" />
        </div>
      </div>
    </div>
  </div>
</template>
