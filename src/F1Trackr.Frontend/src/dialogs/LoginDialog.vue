<script setup lang="ts">
import LoginModal from '@/modals/LoginModal.vue'
import useHttp from '@/utils/http.ts'
import { ref } from 'vue'

const http = useHttp()

const request = ref<{
  userName?: string
  accessCode?: string
}>({})

const hasErrors = ref<boolean>(false)
const errors = ref<Record<string, string[]>>({})

async function login() {
  const response = await http.post(`/api/account/login`, {
    userName: request.value.userName,
    accessCode: request.value.accessCode,
  })

  if (response.success) {
    window.location.reload()
  } else {
    hasErrors.value = true
    errors.value = response.errors!
  }
}
</script>

<template>
  <LoginModal v-model:request="request" :has-errors="hasErrors" :errors="errors" @login="login" />
</template>
