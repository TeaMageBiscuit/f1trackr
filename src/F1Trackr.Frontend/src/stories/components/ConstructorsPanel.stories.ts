import ConstructorsPanel from '@/components/ConstructorsPanel.vue'
import { constructors } from '@/stories/SampleData'
import type { Meta, StoryObj } from '@storybook/vue3-vite'

const meta = {
  title: 'Components/Constructors Panel',
  component: ConstructorsPanel,
  args: {
    loading: false,
    constructors: constructors,
  },
} satisfies Meta<typeof ConstructorsPanel>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    constructors: constructors,
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    constructors: [],
  },
}

export const Empty: Story = {
  args: {
    loading: false,
    constructors: [],
  },
}
