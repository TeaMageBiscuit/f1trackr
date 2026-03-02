// This file has been automatically migrated to valid ESM format by Storybook.
import type { StorybookConfig } from '@storybook/vue3-vite'
import path from 'path'
import { fileURLToPath } from 'url'
import { mergeConfig } from 'vite'

const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

const config: StorybookConfig = {
  stories: ['../src/**/*.stories.@(js|jsx|mjs|ts|tsx)'],
  core: {
    builder: {
      name: '@storybook/builder-vite',
      options: {
        viteConfigPath: path.resolve(__dirname, '../vite.config.ts'),
      },
    },
  },
  async viteFinal(config) {
    return mergeConfig(config, {
      resolve: {
        alias: {
          '@': path.resolve(__dirname, '../src'),
        },
      },
    })
  },
  addons: ['@storybook/addon-themes'],
  framework: {
    name: '@storybook/vue3-vite',
    options: {
      docgen: {
        plugin: 'vue-component-meta',
        tsconfig: 'tsconfig.app.json',
      },
    },
  },
}
export default config
