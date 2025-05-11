import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../HomePage.vue'
import UsersPage from '../UsersPage.vue'

const routes = [
  { path: '/', name: 'Home', component: HomeView },
  { path: '/users', name: 'Users', component: UsersPage }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router