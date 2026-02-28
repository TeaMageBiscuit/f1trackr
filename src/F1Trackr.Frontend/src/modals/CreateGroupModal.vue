<script setup lang="ts">
import Button from 'primevue/button'
import IftaLabel from 'primevue/iftalabel'
import InputText from 'primevue/inputtext'
import Message from 'primevue/message'
import Select from 'primevue/select'

const request = defineModel<{
  name?: string | null
  season?: number | null
}>('request', { default: {} })

defineProps<{
  seasons: number[]
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
      <div>Failed to create group.</div>

      <ul class="list-disc pl-5">
        <li v-for="(line, index) in errors['']" :key="index">
          {{ line }}
        </li>
      </ul>
    </Message>

    <IftaLabel>
      <InputText id="name" v-model="request.name" fluid :invalid="(errors['Name']?.length ?? 0) > 0" />
      <label for="name">Group Name</label>
      <Message v-if="errors['Name']" size="small" severity="error" variant="simple">
        {{ errors['Name']?.join(' ') }}
      </Message>
    </IftaLabel>

    <IftaLabel>
      <Select
        id="season"
        v-model="request.season"
        :options="seasons"
        fluid
        :invalid="(errors['Season']?.length ?? 0) > 0"
      />
      <label for="season">Season</label>
      <Message v-if="errors['Season']" size="small" severity="error" variant="simple">
        {{ errors['Season']?.join(' ') }}
      </Message>
    </IftaLabel>

    <div class="flex justify-center gap-2">
      <Button label="Create Group" class="w-1/3" @click="$emit('create')" />
      <Button label="Cancel" severity="secondary" outlined class="w-1/3" @click="$emit('cancel')" />
    </div>
  </div>
</template>
