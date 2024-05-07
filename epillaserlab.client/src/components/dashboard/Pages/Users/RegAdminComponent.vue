<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card">
          <div class="card-header">Регистрация администратора</div>
          <div class="card-body">
            <form @submit.prevent="register">
              <div class="form-group">
                <label for="branchId">Филиал:</label>
                <select v-model="branchId" class="form-control" required>
                  <option v-for="branch in branches" :key="branch.branchId" :value="branch.branchId">
                    {{ branch.address }}
                  </option>
                </select>
              </div>
              <div class="form-group mt-3">
                <label for="surname">Фамилия:</label>
                <input type="text" v-model="surname" class="form-control" required>
              </div>
              <div class="form-group mt-3">
                <label for="name">Имя:</label>
                <input type="text" v-model="name" class="form-control" required>
              </div>
              <div class="form-group mt-3">
                <label for="patronymic">Отчество:</label>
                <input type="text" v-model="patronymic" class="form-control">
              </div>
              <div class="form-group mt-3">
                <label for="login">Логин:</label>
                <input type="text" v-model="login" class="form-control" required>
              </div>
              <div class="form-group mt-3">
                <label for="password">Пароль:</label>
                <input type="password" v-model="password" class="form-control" required>
              </div>
              <button type="submit" class="btn btn-success mt-3">Зарегистрировать</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

export default {
  data() {
    return {
      branches: [],
      branchId: null,
      surname: '',
      name: '',
      patronymic: '',
      login: '',
      password: ''
    }
  },
  mounted() {
    this.loadBranches();
  },
  setup(){
    const router = useRouter();
    const toast = useToast();
    return {router, toast}
  },
  methods: {
    loadBranches() {
      const apiUrl = 'https://localhost:7243/api/branches?limit=9999';
      fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
          this.branches = data.data.recs;
          this.branchId = data.data.recs[0].branchId;
        })
        .catch(error => console.error(error));
    },
    async register() {

      const apiUrl = 'https://localhost:7243/api/auth/register/admins';
      const data = {
        branchId: this.branchId,
        surname: this.surname,
        name: this.name,
        patronymic: this.patronymic,
        login: this.login,
        password: this.password
      };
      const response = await fetch(apiUrl, {
        method: "POST",
        headers: {'Content-Type': "application/json"},
        credentials: "include",
        body: JSON.stringify(data)
      });
      
      const result = await response.json();
      if(result.message == "OK"){
        this.toast.success("Запись успешно добавлена");
        this.router.push({name: "dashboard.users", params: {role: "admin"}})
      } else if(result.message == "DUPLICATION"){
        this.toast.error("Дублирование записи");
      } else if(result.message == "NOT CHANGED"){
        this.toast.info("Внесите изменения");
      } else if(result.message == "DATA NOT VALID" || result.status == 400){
        this.toast.error("Введите значения");
      } else {
        this.toast.error("Ошибка при добавлении записи");
      }
    }
  }
}
</script>