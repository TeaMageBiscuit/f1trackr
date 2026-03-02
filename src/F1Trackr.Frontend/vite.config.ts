import tailwindcss from '@tailwindcss/vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'
import { fileURLToPath } from 'url'
import { defineConfig } from 'vite'

const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

// https://vite.dev/config/
export default defineConfig({
  plugins: [tailwindcss(), vue()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    host: true,
    port: Number(process.env.PORT) || 5173,
    strictPort: true,
    proxy: {
      '/api': {
        target: process.env.BACKEND_URL,
        changeOrigin: true,
        secure: false,
      },
    },
  },
})
