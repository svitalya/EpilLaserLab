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
                    'path': '/dashboard/home',
                    'name': 'dashboard.home',
                    component: () => import('./components/dashboard/Pages/HomeComponent.vue')
                },
                {
                    'path': '/dashboard/references',
                    'name': 'dashboard.references',
                    component: () => import('./components/dashboard/Pages/ReferencesComponent.vue')
                },
                {
                    path: '/dashboard/reference/:referencename',
                    name: 'dashboard.reference',
                    component: () => import('./components/dashboard/Pages/Reference/ShowListComponent.vue')
                },
                {     
                    path: '/dashboard/reference/:referencename/add',
                    name: 'dashboard.reference.add',
                    component: () => import('./components/dashboard/Pages/Reference/AddComponent.vue')  
                },
                {     
                    path: '/dashboard/reference/:referencename/edit/:id',
                    name: 'dashboard.reference.edit',
                    component: () => import('./components/dashboard/Pages/Reference/EditComponent.vue')  
                },
                {
                    'path': '/dashboard/services',
                    'name': 'dashboard.services',
                    component: () => import('./components/dashboard/Pages/Services/ShowListComponent.vue')
                },
                {
                    'path': '/dashboard/service/add',
                    'name': 'dashboard.service.add',
                    component: () => import('./components/dashboard/Pages/Services/AddComponent.vue')
                },
                {
                    'path': '/dashboard/service/edit/:id',
                    'name': 'dashboard.service.edit',
                    component: () => import('./components/dashboard/Pages/Services/EditComponent.vue')
                },
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