<template>
<div class="text-center">
    <form style="width: 22rem;" class="position-absolute top-50 start-50 translate-middle" @submit.prevent="submit">
        <!-- Email input -->
        <div class="form-outline mb-4">
            <label class="form-label" for="login">Логин</label>
            <input v-model="data.login" type="text" id="login" class="form-control" required/> 
        </div>

        <!-- Password input -->
        <div class="form-outline mb-4">
            <label class="form-label" for="password">Пароль</label>
            <input v-model="data.password" type="password" id="password" class="form-control" required/>
        </div>

        <!-- 2 column grid layout for inline styling -->
        <!-- <div class="row mb-4">
            <div class="col d-flex justify-content-center">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" value="" id="form2Example31" checked />
                    <label class="form-check-label" for="form2Example31"> Remember me </label>
                </div>
            </div>


            <div class="col">
                <a href="#!">Forgot password?</a>
            </div>
        </div> -->

        <!-- Submit button -->
        <button type="submit" class="btn btn-primary btn-block mb-4">Войти</button>
    </form>
</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { reactive } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from "vue-toastification";

export default defineComponent({
    name: "Login",
    setup(){
        const data = reactive({
            login: "",
            password: ""
        });

        const router = useRouter();
        const toast = useToast();

        const submit = async () => {
            const response = await fetch("https://localhost:7243/api/auth/login", {
                method: "POST",
                headers: {'Content-Type': "application/json"},
                credentials: "include",
                body: JSON.stringify(data)
            });

            if(response.ok){
                toast.success("Вход выполнен", {
                    timeout: 5000,
                });

                await router.push("/dashboard/home");
            }else{
                toast.error("Нет пользователя с таким логином и паролем", {
                    timeout: 5000,
                });
            }
        }

        return {data, submit, toast}
    }
});
</script>

<style>
</style>