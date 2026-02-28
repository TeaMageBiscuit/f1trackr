import SeasonBrowserPage from '@/pages/SeasonBrowserPage.vue'
import { constructors, drivers, races } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'

const meta = {
  title: 'Pages/Season Browser Page',
  component: SeasonBrowserPage,
  args: {
    currentSeason: 2026,
    seasons: [2026, 2025],
    constructors: constructors,
    drivers: drivers,
    races: races,
  },
} satisfies Meta<typeof SeasonBrowserPage>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
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
    constructors: [],
    drivers: [],
    races: [],
  },
}
