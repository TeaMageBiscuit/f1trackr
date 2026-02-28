import CreateGroupModal from '@/modals/CreateGroupModal.vue'
import { renderModalInFrame } from '@/stories/ModalRender.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Modals/Create Group',
  component: CreateGroupModal,
  args: {
    onCreate: () => action('create')(),
    onCancel: () => action('cancel')(),
  },
  render: renderModalInFrame(CreateGroupModal),
} satisfies Meta<typeof CreateGroupModal>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    seasons: [2026, 2025],
    hasErrors: false,
    errors: {},
    request: {
      name: '',
      season: 2026,
    },
  },
}

export const Error: Story = {
  args: {
    seasons: [2026, 2025],
    hasErrors: true,
    errors: {
      Name: ['Group Name is already in use'],
    },
    request: {
      name: 'admin',
      season: 2025,
    },
  },
}
