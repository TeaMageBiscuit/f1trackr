import AddUserToGroupModal from '@/modals/AddUserToGroupModal.vue'
import { renderModalInFrame } from '@/stories/ModalRender.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Modals/Add User To Group',
  component: AddUserToGroupModal,
  args: {
    onAdd: () => action('add')(),
    onCancel: () => action('cancel')(),
  },
  render: renderModalInFrame(AddUserToGroupModal),
} satisfies Meta<typeof AddUserToGroupModal>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loadingUsers: false,
    users: [
      { id: { value: 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa' }, name: 'Alice', isAdmin: false },
      { id: { value: 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb' }, name: 'Bob', isAdmin: true },
      { id: { value: 'cccccccc-cccc-cccc-cccc-cccccccccccc' }, name: 'Charlie', isAdmin: false },
    ],
    hasErrors: false,
    errors: {},
    request: {
      user: null,
    },
  },
}

export const Error: Story = {
  args: {
    loadingUsers: false,
    users: [{ id: { value: 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb' }, name: 'Bob', isAdmin: true }],
    hasErrors: true,
    errors: {
      UserId: ['User is already a member of this group.'],
    },
    request: {
      user: { id: { value: 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb' }, name: 'Bob', isAdmin: true },
    },
  },
}

export const LoadingUsers: Story = {
  args: {
    loadingUsers: true,
    users: [],
    hasErrors: false,
    errors: {},
    request: {
      user: null,
    },
  },
}
