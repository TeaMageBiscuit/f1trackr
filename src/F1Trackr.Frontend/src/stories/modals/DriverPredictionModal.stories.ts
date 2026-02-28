import DriverPredictionModal from '@/modals/DriverPredictionModal.vue'
import { renderModalInFrame } from '@/stories/ModalRender.ts'
import { drivers } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Modals/Driver Prediction',
  component: DriverPredictionModal,
  args: {
    loading: false,
    hasErrors: false,
    errors: {},
    modelValue: drivers,
    onSave: (positions) => action('onSave')(positions),
    onCancel: () => action('onCancel')(),
  },
  render: renderModalInFrame(DriverPredictionModal),
} satisfies Meta<typeof DriverPredictionModal>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {}
