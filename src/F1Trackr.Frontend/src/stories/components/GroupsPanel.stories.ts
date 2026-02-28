import GroupsPanel from '@/components/GroupsPanel.vue'
import { groupSummaries } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Components/Groups Panel',
  component: GroupsPanel,
  args: {
    loading: false,
    groups: groupSummaries,
    onCreate: () => action('create')(),
    onView: (group) => action('view')(group),
    onDelete: (group) => action('delete')(group),
  },
} satisfies Meta<typeof GroupsPanel>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    groups: groupSummaries,
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    groups: [],
  },
}

export const Empty: Story = {
  args: {
    loading: false,
    groups: [],
  },
}
