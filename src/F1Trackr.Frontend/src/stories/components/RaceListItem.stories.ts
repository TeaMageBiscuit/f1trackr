import RaceListItem from '@/components/RaceListItem.vue'
import { races } from '@/stories/SampleData'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Components/Race List Item',
  component: RaceListItem,
  args: {
    loading: false,
    race: races[0],
    onSelect: (race) => action('select')(race),
  },
} satisfies Meta<typeof RaceListItem>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    race: races[0],
  },
}

export const AnotherRace: Story = {
  args: {
    loading: false,
    race: races[1],
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    race: undefined,
  },
}
