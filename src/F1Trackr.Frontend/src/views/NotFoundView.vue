<script setup lang="ts">
import Button from 'primevue/button'
import { useRouter } from 'vue-router'

const router = useRouter()

function goHome() {
  router.push({ name: 'home' })
}

function goBack() {
  router.back()
}
</script>

<template>
  <div class="mx-auto w-full max-w-6xl">
    <div class="flex min-h-[60vh] items-center justify-center">
      <div class="card w-full max-w-xl p-10!">
        <div class="flex items-start gap-3">
          <i class="pi pi-map-marker text-3xl text-surface-500" />
          <div class="flex-1">
            <div class="text-xl font-semibold">Page not found</div>
            <p class="mt-1 text-surface-600 dark:text-surface-300">
              The page you’re looking for doesn’t exist (or it did a pit stop and never came back).
            </p>
          </div>
        </div>

        <div class="mt-4 flex flex-wrap gap-2">
          <Button icon="pi pi-home" label="Go to Home" @click="goHome" />
          <Button icon="pi pi-arrow-left" label="Go Back" severity="secondary" outlined @click="goBack" />
        </div>
      </div>
    </div>
  </div>
</template>
