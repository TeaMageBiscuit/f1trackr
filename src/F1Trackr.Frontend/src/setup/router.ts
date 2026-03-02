import type { App } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'

import type { Account } from '@/models'
import HomeView from '@/views/HomeView.vue'

const AdminView = () => import('@/views/AdminView.vue')
const AdminGroupView = () => import('@/views/AdminGroupView.vue')
const AdminGroupMemberView = () => import('@/views/AdminGroupMemberView.vue')
const GroupsView = () => import('@/views/GroupsView.vue')
const GroupView = () => import('@/views/GroupView.vue')
const GroupMemberView = () => import('@/views/GroupMemberView.vue')
const AccessDeniedView = () => import('@/views/AccessDeniedView.vue')
const NotFoundView = () => import('@/views/NotFoundView.vue')

const routes = [
  { name: 'home', path: '/', component: HomeView },
  { name: 'admin', path: '/admin', component: AdminView, meta: { requiresAdmin: true } },
  { name: 'admin-group', path: '/admin/groups/:groupId', component: AdminGroupView, meta: { requiresAdmin: true } },
  {
    name: 'admin-group-member',
    path: '/admin/groups/:groupId/members/:userId',
    component: AdminGroupMemberView,
    meta: { requiresAdmin: true },
  },
  { name: 'groups', path: '/groups', component: GroupsView, meta: { requiresAuth: true } },
  { name: 'group', path: '/groups/:groupId', component: GroupView, meta: { requiresAuth: true } },
  {
    name: 'group-member',
    path: '/groups/:groupId/members/:userId',
    component: GroupMemberView,
    meta: { requiresAuth: true },
  },
  {
    name: 'race-details',
    path: '/seasons/:season/races/:round',
    component: () => import('@/views/RaceDetailsView.vue'),
  },
  { name: 'access-denied', path: '/access-denied', component: AccessDeniedView },
  { name: 'not-found', path: '/:pathMatch(.*)*', component: NotFoundView },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

let cachedAccount: Account | null = null
let pendingAccount: Promise<Account> | null = null

async function getAccount(): Promise<Account> {
  if (cachedAccount) return cachedAccount
  if (pendingAccount) return pendingAccount

  pendingAccount = fetch('/api/account/me')
    .then(async (response) => await response.json())
    .then((account) => account)
    .catch(() => {})
    .finally(() => {
      pendingAccount = null
    })

  cachedAccount = await pendingAccount
  return cachedAccount
}

router.beforeEach(async (to) => {
  const account: Account = await getAccount()

  const requiresAdmin = to.matched.some((r) => r.meta?.requiresAdmin)
  const requiresAuth = to.matched.some((r) => r.meta?.requiresAuth)

  if (!requiresAdmin && !requiresAuth) return true

  if (requiresAdmin) {
    if (account?.isAdmin) return true
  }

  if (requiresAuth) {
    if (account?.isAuthenticated) return true
  }

  return {
    name: 'access-denied',
    query: { from: to.fullPath },
  }
})

const setupVueRouter = (app: App<Element>) => {
  app.use(router)
}

export default setupVueRouter
