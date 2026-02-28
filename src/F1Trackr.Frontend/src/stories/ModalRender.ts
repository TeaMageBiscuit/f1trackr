import type { Meta } from '@storybook/vue3-vite'
import type { Component } from 'vue'

export function renderModalInFrame<TComponent extends Component>(
  component: TComponent,
): NonNullable<Meta<TComponent>['render']> {
  return (args: Record<string, unknown>) => ({
    setup() {
      return { args, Component: component }
    },
    template: `
      <div class="w-1/2 mx-auto my-10 p-5 border rounded-xl border-surface-300 dark:border-surface-600">
        <component :is="Component" v-bind="args" />
      </div>
    `,
  })
}
