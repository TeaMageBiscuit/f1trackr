import ConstructorListItem from '@/components/ConstructorListItem.vue'
import { constructors } from '@/stories/SampleData'
import type { Meta, StoryObj } from '@storybook/vue3-vite'

const meta = {
  title: 'Components/Constructor List Item',
  component: ConstructorListItem,
  args: {
    loading: false,
    emphasize: true,
    constructor: constructors[0],
  },
} satisfies Meta<typeof ConstructorListItem>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
    emphasize: true,
    constructor: constructors[0],
  },
}

export const NotEmphasized: Story = {
  args: {
    loading: false,
    emphasize: false,
    constructor: constructors[1],
  },
}

export const Loading: Story = {
  args: {
    loading: true,
    emphasize: true,
    constructor: undefined,
  },
}

export const MissingConstructor: Story = {
  args: {
    loading: false,
    emphasize: true,
    constructor: undefined,
  },
}

export const UnknownNationality: Story = {
  args: {
    loading: false,
    emphasize: true,
    constructor: {
      id: {
        value: 'unknown',
      },
      name: 'Mystery Racing',
      nationality: 'Martian',
      logoAddress: null,
    },
  },
}
