import AdminPage from '@/pages/AdminPage.vue'
import { groupSummaries, users } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Pages/Admin',
  component: AdminPage,
  args: {
    loading: false,
    seasons: [2026, 2025],
    onCreateUser: () => action('createUser')(),
    onUpdateUser: (user) => action('updateUser')(user),
    onDeleteUser: (user) => action('deleteUser')(user),
    onCreateGroup: () => action('createGroup')(),
    onViewGroup: (group) => action('viewGroup')(group),
    onDeleteGroup: (group) => action('deleteGroup')(group),
  },
} satisfies Meta<typeof AdminPage>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    users: users,
    groups: groupSummaries,
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    users: [],
    groups: [],
  },
}
