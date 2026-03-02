import RacesPanel from '@/components/RacesPanel.vue'
import { races } from '@/stories/SampleData'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Components/Races Panel',
  component: RacesPanel,
  args: {
    loading: false,
    races: races,
    onSelect: (race) => action('select')(race),
  },
} satisfies Meta<typeof RacesPanel>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    races: races,
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    races: [],
  },
}

export const Empty: Story = {
  args: {
    loading: false,
    races: [],
  },
}
