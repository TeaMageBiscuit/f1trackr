import AppLayout from '@/components/AppLayout.vue'
import type { Account } from '@/models'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const guestAccount: Account = {
  isAuthenticated: false,
  isAdmin: false,
}

const userAccount: Account = {
  userName: 'jane.doe',
  isAuthenticated: true,
  isAdmin: false,
}

const adminAccount: Account = {
  userName: 'admin',
  isAuthenticated: true,
  isAdmin: true,
}

const meta = {
  title: 'Components/App Layout',
  component: AppLayout,
  args: {
    darkMode: false,
    account: guestAccount,
    canBootstrap: false,
    onNavigate: (to) => action('navigate')(to),
    onToggleTheme: () => action('toggle-theme')(),
    onLogin: () => action('login')(),
    onLogout: () => action('logout')(),
    onBootstrap: () => action('bootstrap')(),
  },
} satisfies Meta<typeof AppLayout>

export default meta
type Story = StoryObj<typeof meta>

export const Guest: Story = {
  args: {
    darkMode: false,
    account: guestAccount,
    canBootstrap: false,
  },
}

export const AuthenticatedUser: Story = {
  args: {
    darkMode: false,
    account: userAccount,
    canBootstrap: false,
  },
}

export const Admin: Story = {
  args: {
    darkMode: false,
    account: adminAccount,
    canBootstrap: false,
  },
}

export const BootstrapAvailable: Story = {
  args: {
    darkMode: false,
    account: guestAccount,
    canBootstrap: true,
  },
}

export const DarkModeProp: Story = {
  args: {
    darkMode: true,
    account: userAccount,
    canBootstrap: false,
  },
}
