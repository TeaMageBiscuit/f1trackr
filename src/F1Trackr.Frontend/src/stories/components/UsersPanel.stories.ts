import UsersPanel from '@/components/UsersPanel.vue'
import { users } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Components/Users Panel',
  component: UsersPanel,
  args: {
    loading: false,
    users: users,
    onCreate: () => action('create')(),
    onUpdate: (user) => action('update')(user),
    onDelete: (user) => action('delete')(user),
  },
} satisfies Meta<typeof UsersPanel>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    users: users,
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    users: [],
  },
}

export const Empty: Story = {
  args: {
    loading: false,
    users: [],
  },
}
