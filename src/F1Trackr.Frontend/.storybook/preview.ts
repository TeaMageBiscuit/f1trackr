import { withThemeByClassName } from '@storybook/addon-themes'
import { type Preview, setup } from '@storybook/vue3-vite'
import { useEffect } from 'storybook/preview-api'
import setupPrimeVue from '../src/setup/primevue'

import '../src/style.css'

const preview: Preview = {
  parameters: {
    options: {
      storySort: {
        method: 'alphabetical',
        order: ['Components', 'Pages', 'Modals'],
      },
    },
    backgrounds: {
      options: {
        transparent: { name: 'transparent', value: '#00000000' },
      },
    },
    controls: {
      matchers: {
        color: /(background|color)$/i,
        date: /Date$/i,
      },
    },
  },

  decorators: [
    withThemeByClassName({
      themes: {
        light: '',
        dark: 'f1-dark',
      },
      defaultTheme: 'light',
      parentSelector: 'html',
    }),
    (storyFn) => {
      useEffect(() => {
        document.documentElement.classList.add('bg-white', 'dark:bg-surface-950')
      }, [])

      return storyFn()
    },
  ],

  initialGlobals: {
    theme: 'light',
    backgrounds: {
      value: 'light',
    },
  },
}

setup((app) => {
  setupPrimeVue(app)
})

export default preview
