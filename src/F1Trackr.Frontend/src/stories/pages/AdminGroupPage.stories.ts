import AdminGroupPage from '@/pages/AdminGroupPage.vue'
import { groupOverview } from '@/stories/SampleData'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta: Meta<typeof AdminGroupPage> = {
  title: 'Pages/Admin Group',
  component: AdminGroupPage,
  args: {
    loading: false,
    onBack: () => action('back')(),
    onRefresh: () => action('refresh')(),
    onAddMember: () => action('addMember')(),
    onViewMember: (member) => action('viewMember')(member),
    onRemoveMember: (member) => action('removeMember')(member),
  },
}

export default meta

type Story = StoryObj<typeof AdminGroupPage>

export const Default: Story = {
  args: {
    loading: false,
    group: groupOverview,
  },
}

export const Loading: Story = {
  args: {
    loading: true,
  },
}

export const Empty: Story = {
  args: {
    loading: false,
    group: {
      ...groupOverview,
      members: [],
    },
  },
}
