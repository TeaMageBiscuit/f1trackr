import UpdateUserModal from '@/modals/UpdateUserModal.vue'
import { renderModalInFrame } from '@/stories/ModalRender.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Modals/Update User',
  component: UpdateUserModal,
  args: {
    onUpdate: () => action('update')(),
    onCancel: () => action('cancel')(),
  },
  render: renderModalInFrame(UpdateUserModal),
} satisfies Meta<typeof UpdateUserModal>

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
