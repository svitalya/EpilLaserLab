import { createApp } from 'vue'
import { createRouter, createWebHistory} from 'vue-router'
import App from './App.vue'
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'home',
            component: () => import('./components/IndexComponent.vue')
        },
        {
            'path': '/login-dashboard',
            'name': 'login-dashboard',
            component: () => import('./components/dashboard/LoginComponent.vue')
        },
        {
            path: '/dashboard',
            name: 'dashboard',
            component: () => import('./components/dashboard/IndexComponent.vue'),
            redirect: to => {
                return {path: '/dashboard/home'}
            },
            children: [
                {
                    'path': 'home',
                    'name': 'dashboard.home',
                    component: () => import('./components/dashboard/Pages/HomeComponent.vue')
                }
            ]
        }
    ]
})

createApp(App)
    .use(router)
    .use(Toast, {
        transition: "Vue-Toastification__bounce",
        maxToasts: 20,
        newestOnTop: true
    })
.mount('#app');