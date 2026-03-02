<script setup lang="ts">
import Button from 'primevue/button'
import IftaLabel from 'primevue/iftalabel'
import InputText from 'primevue/inputtext'
import Message from 'primevue/message'
import Password from 'primevue/password'

const request = defineModel<{
  userName?: string | null
  accessCode?: string | null
}>('request', { default: {} })

defineProps<{
  hasErrors: boolean
  errors: Record<string, string[]>
}>()

defineEmits<{
  (e: 'login'): void
}>()
</script>

<template>
  <div class="flex flex-col gap-4">
    <Message v-if="hasErrors" class="mt-1" severity="error">
      <div>Failed to login.</div>

      <ul class="list-disc pl-5">
        <li v-for="(line, index) in errors['']" :key="index">
          {{ line }}
        </li>
      </ul>
    </Message>

    <IftaLabel>
      <InputText id="userName" v-model="request.userName" fluid :invalid="(errors['UserName']?.length ?? 0) > 0" />
      <label for="userName">User Name</label>
      <Message v-if="errors['UserName']" size="small" severity="error" variant="simple">
        {{ errors['UserName']?.join(' ') }}
      </Message>
    </IftaLabel>

    <IftaLabel>
      <Password
        id="accessCode"
        v-model="request.accessCode"
        fluid
        :feedback="false"
        toggle-mask
        :invalid="(errors['AccessCode']?.length ?? 0) > 0"
      />
      <label for="accessCode">Access Code</label>
      <Message v-if="errors['AccessCode']" size="small" severity="error" variant="simple">
        {{ errors['AccessCode']?.join(' ') }}
      </Message>
    </IftaLabel>

    <Button label="Login" class="w-1/2 mx-auto" @click="$emit('login')" />
  </div>
</template>
