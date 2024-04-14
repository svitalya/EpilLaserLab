<template>
<nav class="navbar navbar-light bg-light p-3">
  <div class="col-10 col-md-10 col-lg-10 d-flex align-items-center justify-content-md-end mt-3 mt-md-0">
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
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';
export default defineComponent({
    data(){
        return {
            user: {login: ""}
        }
    },

    setup(){
        const router = useRouter();
        const toast = useToast();
        const logoutBtnClic = async () => {
            const response = await fetch("https://localhost:7243/api/auth/logout", {
                method: "POST",
                credentials: "include"
            });
            await router.push("/login-dashboard");
        }
        return {router, logoutBtnClic};
    },

    async beforeCreate(){
        
        fetch("https://localhost:7243/api/dashboard", {
            method: "GET",
            headers: {'Content-Type': "application/json"},
            credentials: "include"
        }).then(async response => {
            let responceJson = await response.json();
            if(responceJson.message == "OK"){     
                fetch("https://localhost:7243/api/auth/user", {
                    method: "GET",
                    headers: {'Content-Type': "application/json"},
                    credentials: "include"
                }).then(async response =>{
                    let responceJson = await response.json();
                    this.user = responceJson.user;
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
