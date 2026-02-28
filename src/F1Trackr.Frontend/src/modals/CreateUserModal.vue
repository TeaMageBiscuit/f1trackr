<script setup lang="ts">
import Button from 'primevue/button'
import Checkbox from 'primevue/checkbox'
import IftaLabel from 'primevue/iftalabel'
import InputText from 'primevue/inputtext'
import Message from 'primevue/message'
import Password from 'primevue/password'

const request = defineModel<{
  name?: string | null
  accessCode?: string | null
  isAdmin?: boolean | null
}>('request', { default: {} })

defineProps<{
  hasErrors: boolean
  errors: Record<string, string[]>
}>()

defineEmits<{
  (e: 'create'): void
  (e: 'cancel'): void
}>()
</script>

<template>
  <div class="flex flex-col gap-4">
    <Message v-if="hasErrors" class="mt-1" severity="error">
      <div>Failed to create user.</div>

      <ul class="list-disc pl-5">
        <li v-for="(line, index) in errors['']" :key="index">
          {{ line }}
        </li>
      </ul>
    </Message>

    <IftaLabel>
      <InputText id="name" v-model="request.name" fluid :invalid="(errors['Name']?.length ?? 0) > 0" />
      <label for="name">User Name</label>
      <Message v-if="errors['Name']" size="small" severity="error" variant="simple">
        {{ errors['Name']?.join(' ') }}
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

    <div class="flex items-center gap-2">
      <Checkbox v-model="request.isAdmin" input-id="isAdmin" :binary="true" />
      <label for="isAdmin" class="select-none">Admin</label>
    </div>

    <div class="flex justify-center gap-2">
      <Button label="Create User" class="w-1/3" @click="$emit('create')" />
      <Button label="Cancel" severity="secondary" outlined class="w-1/3" @click="$emit('cancel')" />
    </div>
  </div>
</template>
