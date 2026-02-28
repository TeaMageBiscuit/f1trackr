import DriversPanel from '@/components/DriversPanel.vue'
import { drivers } from '@/stories/SampleData'
import type { Meta, StoryObj } from '@storybook/vue3-vite'

const meta = {
  title: 'Components/Drivers Panel',
  component: DriversPanel,
  args: {
    loading: false,
    drivers: drivers,
  },
} satisfies Meta<typeof DriversPanel>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    drivers: drivers,
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    drivers: [],
  },
}

export const Empty: Story = {
  args: {
    loading: false,
    drivers: [],
  },
}
