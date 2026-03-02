import { createApp } from 'vue'

import App from './App.vue'
import setupPrimeVue from './setup/primevue.ts'
import setupVueRouter from './setup/router.ts'
import './style.css'

const app = createApp(App)

setupPrimeVue(app)
setupVueRouter(app)

app.mount('#app')
