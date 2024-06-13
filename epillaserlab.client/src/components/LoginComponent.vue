<template>

    <!-- <form style="width: 22rem;" class="position-absolute top-50 start-50 translate-middle" @submit.prevent="submit">
        <div class="form-outline mb-4">
            <label class="form-label" for="login">Логин</label>
            <input v-model="data.login" type="text" id="login" class="form-control" required/> 
        </div>

        <div class="form-outline mb-4">
            <label class="form-label" for="password">Пароль</label>
            <input v-model="data.password" type="password" id="password" class="form-control" required/>
        </div>

        <button type="submit" class="btn btn-primary btn-block mb-4">Войти</button>
    </form> -->

    <div id="loginForm" >
        <div class="container">
            <div class="tabs">
                <div class="tab active" data-tab="login">Вход</div>
                <div class="tab" data-tab="register">Регистрация</div>
            </div>
            <div class="tab-content active" id="login">
                <form @submit.prevent="submitLogin">
                    <div class="form-group">
                        <input type="text" v-model="dataLogin.login" class="form-control" placeholder="Логин">
                    </div>
                    <div class="form-group">
                        <input type="password" v-model="dataLogin.password" class="form-control" placeholder="Пароль">
                    </div>
                    <button class="btn">Вход</button>
                </form>
            </div>
            <div class="tab-content" id="register">
                <form @submit.prevent="submitRegister">
                    <div class="form-group">
                        <input type="text" v-model="dataRegister.name" class="form-control" placeholder="Имя">
                    </div>
                    <div class="form-group">
                        <input type="text" v-model="dataRegister.phone" class="form-control" placeholder="Телефон">
                    </div>
                    <div class="form-group">
                        <input type="text" v-model="dataRegister.login" class="form-control" placeholder="Логин">
                    </div>
                    <div class="form-group">
                        <input type="password" v-model="dataRegister.password" class="form-control" placeholder="Пароль">
                    </div>
                    <div class="form-group">
                        <input type="email" v-model="dataRegister.email"  class="form-control" placeholder="Электронная почта">
                    </div>
                    <button class="btn" type="submit">Регистрация</button>
                </form>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { reactive } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from "vue-toastification";
import UserStore from './Pages/Utils/UserStore';

export default defineComponent({
    name: "Login",
    setup(){
        const dataRegister = reactive({
            name: "",
            phone: "",
            email: "",
            login: "",
            password: ""
        });

        const dataLogin = reactive({
            login: "",
            password: ""
        })

        const router = useRouter();
        const toast = useToast();

        const submitRegister = async () => {
            if (!dataRegister.email){
                dataRegister.email = null;
            }

            fetch(`/api/auth/register/clients`, {
                method: "POST",
                headers: {'Content-Type': "application/json"},
                credentials: "include",
                body: JSON.stringify(dataRegister)
            }).then(async response => {
                let result = await response.json();
                if(result.message == "OK"){
                    
                    dataLogin.login = dataRegister.login;
                    dataLogin.password = dataRegister.password;
                    await submitLogin();
                }else if(result.message == "BLOCK"){
                    toast.warning("Данный логин уже занят");
                }
            });

        }

        const submitLogin = async () => {
            fetch(`/api/auth/login`, {
                method: "POST",
                headers: {'Content-Type': "application/json"},
                credentials: "include",
                body: JSON.stringify(dataRegister)
            }).then(async response => {
                let result = await response.json();
                if(result.message == "OK"){
                    toast.success("Вход выполнен");
                    router.push({name: "home"});
                }else if(result.message == "INVALID CREDENTIALS"){
                    toast.warning("Пользователя с таким логином и паролем нет");
                }
            });
        }

        return {dataRegister, dataLogin, submitRegister, toast, submitLogin}
    },

    mounted(){
        const tabs = document.querySelectorAll('.tab');
        const tabContents = document.querySelectorAll('.tab-content');

        tabs.forEach((tab) => {
            tab.addEventListener('click', () => {
                tabs.forEach((tab) => tab.classList.remove('active'));
                tab.classList.add('active');

                const tabId = tab.getAttribute('data-tab');
                tabContents.forEach((tabContent) => {
                    tabContent.classList.remove('active');
                    if (tabContent.id === tabId) {
                        tabContent.classList.add('active');
                    }
                });
            });
        });
    }
});
</script>

<style>
#loginForm{
    background-color: #8A8099; width: 100vw; height: 100vh; font-family: 'EB Garamond', serif;
    padding-top: 40px;
    .container {
            max-width: 400px;
            margin: 0 auto;
            padding: 20px;
            background-color: #4D396A;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .tabs {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
        }

        .tab {
            flex: 1;
            text-align: center;
            padding: 10px;
            border-bottom: 2px solid transparent;
            cursor: pointer;
            color: white;
        }

        .tab.active {
            border-bottom: 2px solid #F7EFB2;
            color: #F7EFB2;
        }

        .tab-content {
            display: none;
            padding: 20px;
        }

        .tab-content.active {
            display: block;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .form-control:focus {
            border-color: #7D6A9A;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .btn {
            width: 100%;
            padding: 10px;
            border: none;
            border-radius: 5px;
            background-color: #7D6A9A;
            color: #fff;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #B0B0B0;
        }
}
</style>