import DriverListItem from '@/components/DriverListItem.vue'
import { drivers } from '@/stories/SampleData'
import type { Meta, StoryObj } from '@storybook/vue3-vite'

const meta = {
  title: 'Components/Driver List Item',
  component: DriverListItem,
  args: {
    loading: false,
    emphasize: true,
    driver: drivers[0],
  },
} satisfies Meta<typeof DriverListItem>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    emphasize: true,
    driver: drivers[0],
  },
}

export const NotEmphasized: Story = {
  args: {
    loading: false,
    emphasize: false,
    driver: drivers[1],
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    emphasize: true,
    driver: undefined,
  },
}

export const MissingDriver: Story = {
  args: {
    loading: false,
    emphasize: true,
    driver: undefined,
  },
}

export const UnknownNationality: Story = {
  args: {
    loading: false,
    emphasize: true,
    driver: {
      id: {
        value: 'unknown',
      },
      code: 'UNK',
      givenName: 'Mystery',
      familyName: 'Driver',
      nationality: 'Martian',
      driverNumber: null,
      imageAddress: null,
    },
  },
}
