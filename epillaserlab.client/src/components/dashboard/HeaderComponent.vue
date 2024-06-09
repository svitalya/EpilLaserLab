<template>
<header class="d-flex flex-wrap justify-content-center py-3 mb-4 border-bottom" >
    <router-link :to="{name: 'dashboard.home'}"class="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-dark text-decoration-none">
        <img v-if="imgSrc" class="bi me-2" width="100" height="100" :src="imgSrc">
        <span class="fs-4">{{ title }}</span>
    </router-link>
    <nav class="navbar navbar-light bg-light p-3">
    <div class="col-12 col-md-12 col-lg-12 d-flex align-items-center justify-content-md-end mt-3 mt-md-0">
        <div class="dropdown">
            <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-expanded="false">
                Доброй пожаловать, {{ user.login }}
            </button>
            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                <li><button class="dropdown-item" @click="logoutBtnClic">Выход </button></li>
            </ul>
        </div>
    </div>
    </nav>
</header>
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';
export default defineComponent({
    data(){
        return {
            user: {login: ""},
            title: "EpilLaserLab",
            imgSrc: ""
        }
    },

    setup(){
        const router = useRouter();
        const toast = useToast();
        const logoutBtnClic = async () => {
            const response = await fetch(`/api/auth/logout`, {
                method: "POST",
                credentials: "include"
            });
            await router.push("/login-dashboard");
        }
        return {router, logoutBtnClic};
    },

    async beforeCreate(){
        
        fetch(`/api/dashboard`, {
            method: "GET",
            headers: {'Content-Type': "application/json"},
            credentials: "include"
        }).then(async response => {
            let responceJson = await response.json();
            if(responceJson.message == "OK"){     
                fetch(`/api/auth/user`, {
                    method: "GET",
                    headers: {'Content-Type': "application/json"},
                    credentials: "include"
                }).then(async response =>{
                    let responceJson = await response.json();
                    this.user = responceJson.user;

                    if(responceJson.user.admin){
                        this.title = `EpilLaserLab - ${responceJson.user.admin.branch.address}`
                        this.imgSrc = `/resources/images/${responceJson.user.admin.branch.photoPath}`
                    }
                    
                });
            }else if(responceJson.message == "ACCESS DENIED"){
                await this.toast.error("Вход в панель не разрешен");
                await this.router.push("/login-dashboard");
            }
        })
        .catch(async () => await this.router.push("/login-dashboard"));
    
    }
})
</script>
