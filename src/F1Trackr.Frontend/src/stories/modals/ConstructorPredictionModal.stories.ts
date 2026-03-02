import ConstructorPredictionModal from '@/modals/ConstructorPredictionModal.vue'
import { renderModalInFrame } from '@/stories/ModalRender.ts'
import { constructors } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Modals/Constructor Prediction',
  component: ConstructorPredictionModal,
  args: {
    loading: false,
    hasErrors: false,
    errors: {},
    modelValue: constructors,
    onSave: (positions) => action('onSave')(positions),
    onCancel: () => action('onCancel')(),
  },
  render: renderModalInFrame(ConstructorPredictionModal),
} satisfies Meta<typeof ConstructorPredictionModal>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {}
