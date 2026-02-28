import { definePreset } from '@primeuix/themes'
import Aura from '@primeuix/themes/aura'
import { DialogService, ToastService } from 'primevue'
import PrimeVue from 'primevue/config'
import type { App } from 'vue'

const Preset = definePreset(Aura, {
  primitive: {
    soho: {
      0: '#ffffff',
      50: '#f4f4f4',
      100: '#e8e9e9',
      200: '#d2d2d4',
      300: '#bbbcbe',
      400: '#a5a5a9',
      500: '#8e8f93',
      600: '#77787d',
      700: '#616268',
      800: '#4a4b52',
      900: '#34343d',
      950: '#1d1e27',
    },
  },
  semantic: {
    primary: {
      50: '#ffecec',
      100: '#ffd6d6',
      200: '#ffafaf',
      300: '#ff7f7f',
      400: '#ff4544',
      500: '#e10600',
      600: '#b60400',
      700: '#8a0200',
      800: '#640100',
      900: '#3d0000',
      950: '#2d0000',
    },
    colorScheme: {
      light: {
        surface: {
          0: '#ffffff',
          50: '{zinc.50}',
          100: '{zinc.100}',
          200: '{zinc.200}',
          300: '{zinc.300}',
          400: '{zinc.400}',
          500: '{zinc.500}',
          600: '{zinc.600}',
          700: '{zinc.700}',
          800: '{zinc.800}',
          900: '{zinc.900}',
          950: '{zinc.950}',
        },
      },
      dark: {
        surface: {
          0: '#ffffff',
          50: '{soho.50}',
          100: '{soho.100}',
          200: '{soho.200}',
          300: '{soho.300}',
          400: '{soho.400}',
          500: '{soho.500}',
          600: '{soho.600}',
          700: '{soho.700}',
          800: '{soho.800}',
          900: '{soho.900}',
          950: '{soho.950}',
        },
      },
    },
  },
})

const setupPrimeVue = (app: App<unknown>) => {
  app.use(DialogService)
  app.use(ToastService)

  app.use(PrimeVue, {
    theme: {
      preset: Preset,
      options: {
        cssLayer: {
          name: 'primevue',
          order: 'theme, base, primevue',
        },
        darkModeSelector: '.f1-dark',
      },
    },
  })
}

export default setupPrimeVue
