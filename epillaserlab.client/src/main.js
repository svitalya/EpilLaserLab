import { createApp } from 'vue'
import { createRouter, createWebHistory} from 'vue-router'
import App from './App.vue'


import DashboardIndex from './components/dashboard/IndexComponent.vue'
import IndexComponentVue from './components/IndexComponent.vue'

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'home',
            component: IndexComponentVue
        },
        {
            path: '/dashboard',
            name: 'dashboard',
            component: DashboardIndex
        }
    ]
})

createApp(App).use(router).mount('#app');