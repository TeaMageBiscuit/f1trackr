import LoginModal from '@/modals/LoginModal.vue'
import { renderModalInFrame } from '@/stories/ModalRender.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'

const meta = {
  title: 'Modals/Login',
  component: LoginModal,
  args: {},
  render: renderModalInFrame(LoginModal),
} satisfies Meta<typeof LoginModal>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    hasErrors: false,
    errors: {},
  },
}

export const Error: Story = {
  args: {
    hasErrors: true,
    errors: {
      UserName: ['Invalid name'],
      AccessCode: ['Invalid access code'],
    },
  },
}
