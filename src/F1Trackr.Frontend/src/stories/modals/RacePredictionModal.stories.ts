import RacePredictionModal from '@/modals/RacePredictionModal.vue'
import { renderModalInFrame } from '@/stories/ModalRender.ts'
import { drivers } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta = {
  title: 'Modals/Race Prediction',
  component: RacePredictionModal,
  args: {
    loading: false,
    hasErrors: false,
    errors: {},
    modelValue: [drivers, []],
    onSave: (positions) => action('onSave')(positions),
    onCancel: () => action('onCancel')(),
  },
  render: renderModalInFrame(RacePredictionModal),
} satisfies Meta<typeof RacePredictionModal>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {}
