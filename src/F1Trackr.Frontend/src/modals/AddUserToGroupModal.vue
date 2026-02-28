<script setup lang="ts">
import type { User } from '@/models'
import AutoComplete, { type AutoCompleteCompleteEvent } from 'primevue/autocomplete'
import Button from 'primevue/button'
import IftaLabel from 'primevue/iftalabel'
import Message from 'primevue/message'
import { computed, ref } from 'vue'

const request = defineModel<{
  user?: User | null
}>('request', { default: {} })

const props = defineProps<{
  users: User[]
  loadingUsers: boolean
  hasErrors: boolean
  errors: Record<string, string[]>
}>()

defineEmits<{
  (e: 'add'): void
  (e: 'cancel'): void
}>()

const suggestions = ref<User[]>([])

const selectedUserId = computed(() => request.value.user?.id.value ?? '')

function onComplete(e: AutoCompleteCompleteEvent) {
  const q = String(e.query ?? '')
    .trim()
    .toLowerCase()

  if (!q) {
    suggestions.value = props.users.slice(0, 25)
    return
  }

  suggestions.value = props.users
    .filter((u) => {
      const id = u.id.value.toLowerCase()
      const name = u.name.toLowerCase()
      return name.includes(q) || id.includes(q)
    })
    .slice(0, 25)
}
</script>

<template>
  <div class="flex flex-col gap-4">
    <Message v-if="hasErrors" class="mt-1" severity="error">
      <div>Failed to add user to group.</div>

      <ul class="list-disc pl-5">
        <li v-for="(line, index) in errors['']" :key="index">
          {{ line }}
        </li>
      </ul>
    </Message>

    <IftaLabel>
      <AutoComplete
        id="user"
        v-model="request.user"
        :suggestions="suggestions"
        option-label="name"
        dropdown
        fluid
        :loading="loadingUsers"
        :invalid="(errors['UserId']?.length ?? 0) > 0"
        placeholder="Search by name or id..."
        @complete="onComplete"
      >
        <template #option="{ option }">
          <div class="flex flex-col">
            <div class="font-medium">{{ option.name }}</div>
            <div class="text-sm text-surface-600 dark:text-surface-300">
              <code class="uppercase">{{ option.id.value }}</code>
            </div>
          </div>
        </template>
      </AutoComplete>

      <label for="user">User</label>

      <Message v-if="errors['UserId']" size="small" severity="error" variant="simple">
        {{ errors['UserId']?.join(' ') }}
      </Message>

      <div v-if="selectedUserId" class="text-xs text-surface-600 dark:text-surface-300 mt-1">
        Selected:
        <code class="uppercase">{{ selectedUserId }}</code>
      </div>
    </IftaLabel>

    <div class="flex justify-center gap-2">
      <Button label="Add Member" class="w-1/3" :disabled="!request.user" @click="$emit('add')" />
      <Button label="Cancel" severity="secondary" outlined class="w-1/3" @click="$emit('cancel')" />
    </div>
  </div>
</template>
