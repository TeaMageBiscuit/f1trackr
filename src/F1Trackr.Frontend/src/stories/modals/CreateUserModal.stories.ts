import CreateUserModal from '@/modals/CreateUserModal.vue'
import { renderModalInFrame } from '@/stories/ModalRender.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Modals/Create User',
  component: CreateUserModal,
  args: {
    onCreate: () => action('create')(),
    onCancel: () => action('cancel')(),
  },
  render: renderModalInFrame(CreateUserModal),
} satisfies Meta<typeof CreateUserModal>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    hasErrors: false,
    errors: {},
    request: {
      name: '',
      accessCode: '',
      isAdmin: false,
    },
  },
}

export const Error: Story = {
  args: {
    hasErrors: true,
    errors: {
      '': ['General Error'],
      Name: ['User Name is already in use'],
    },
    request: {
      name: 'admin',
      accessCode: '',
      isAdmin: false,
    },
  },
}
