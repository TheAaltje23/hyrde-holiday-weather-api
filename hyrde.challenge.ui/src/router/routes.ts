import { RouteRecordRaw } from 'vue-router'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/LayoutWeatherApp.vue'),
    children: [
      { path: '', name: 'homepage', component: () => import('pages/IndexWeatherApp.vue') },
      { path: 'login', name: 'login', component: () => import('pages/LoginWeatherApp.vue') }
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue')
  }
]

export default routes
