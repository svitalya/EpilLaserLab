<template>
<div class="row mt-3">
  <form @submit.prevent="submitForm" class = "col-3">
    <div class="row form-group mt-3">
      <label class="h5">Сервер</label>
      <div class="col-12">
        <input type="text" v-model="data.server" class="form-control" placeholder="Введите адрес сервера">
      </div>
    </div>
    <div class="row form-group mt-3">
      <label class="h5">Название</label>
      <div class="col-12">
        <input type="text" v-model="data.database" class="form-control" placeholder="Введите название">
      </div>
    </div>
    <div class="row form-group mt-3">
      <label class="h5">Логин</label>
      <div class="col-12">
        <input type="text" v-model="data.user" class="form-control" placeholder="Введите логин">
      </div>
    </div>
    <div class="row form-group mt-3">
      <label class="h5">Пароль</label>
      <div class="col-12">
        <input type="password" v-model="data.password" class="form-control" placeholder="Введите пароль">
      </div>
    </div>
    <div class="row form-group mt-3">
      <label class="h5">Порт</label>
      <div class="col-12">
        <input type="text" v-model="data.port" class="form-control" placeholder="Введите порт">
      </div>
    </div>
    <div class="row form-group mt-4">
      <button type="submit" class="btn btn-success" style="width: 120px;">Сохранить</button>
    </div>
  </form>
  <div class = "col-3 ms-4">
    <form @submit.prevent="submitRestoreDump">
      <div class="row form-group mt-3">
        <label class="h5">Копия БД</label>
        <div class="input-group mb-3">
          <input type="file" class="form-control" v-on:change="e => {if (e.target.files.length > 0) getBase64(e.target)}">
          <label class="input-group-text">Загрузка</label>
        </div>
      </div>
      <div class="row form-group">
        <button type="submit" class="btn btn-success" style="width: 120px;">Восстановить</button>
        <button type="button" @click="getDump" class="btn btn-secondary ms-3" style="width: 120px;">Получить копию</button>
      </div>
    </form>
  </div>
</div>
</template>
<script lang="ts">
import { defineComponent, reactive} from 'vue';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';
import {saveAs} from 'file-saver';

export default defineComponent({
  setup(){
    const toast = useToast();
    const router = useRouter();

    const data = reactive({
        server: null,
        database: null,
        user: null,
        password: null,
        port: null
    });

    const restoreDumpData = reactive({
      dumpBase64: null
    })

    const submitForm = async () => {
      await fetch(`/api/db/change`, {
        method: "POST",
        headers: {'Content-Type': "application/json"},
        credentials: "include",
        body: JSON.stringify(data)
      }).then(async response => {
          const result = await response.json();
          if(result.message == "OK"){
            toast.success("Данные подключения к БД успешно изменены. Необходимо переавторизоваться");

            data.server = result.data.server;
            data.database = result.data.database;
            data.user = result.data.user;
            data.password = result.data.password;
            data.port = result.data.port;

            router.push({name: "login-dashboard"});
          }else if(result.message == "ERROR"){
            toast.error("Выбранная база данных не является допустимой");
          }
          else if(result.message == "DATA NOT VALID"){
            toast.error("Не удалось подключится к БД");
          }
        }).catch(r => router.push({name: "dashboard"}))
    }

    const getDump = async() => {
      await fetch(`/api/db/dump`, {
        method: "POST",
        headers: {'Content-Type': "application/json"},
        credentials: "include"
      }).then(async response => {
          const result = await response.json();
          if(result.message == "OK"){          
            window.open(result.dump, "_blank")
          }else{
            toast.error("Произошла неизвестная ошибка");
          }
        }).catch(r => router.push({name: "dashboard"}))
    }

    const getBase64 = (target) => {
        let file = target.files[0]; 
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function () {
          let base64Data = (<String>reader.result).split(";");
          let format = base64Data[0];
  
          if(["data:application/octet-stream"].indexOf(format) != -1){
            restoreDumpData.dumpBase64 = base64Data[1].replace('base64,', '');
  
          }else{
            target.value = null;
            toast.error("Недопустимый тип файла");
          }
        };
        reader.onerror = function (error) {
          console.log('Error: ', error);
        };
      }

      const submitRestoreDump = async () => {
        if(!restoreDumpData.dumpBase64){
          toast.info("Загрузите файл БД");
          return;
        }

        await fetch(`/api/db/restore`, {
          method: "POST",
          headers: {'Content-Type': "application/json"},
          credentials: "include",
          body: JSON.stringify(restoreDumpData)
        }).then(async response => {
            const result = await response.json();
            if(result.message == "OK"){
              toast.success("Данные БД успешно восстановлены. Необходимо переавторизоваться");
              router.push({name: "login-dashboard"});
            }else if(result.message == "ERROR"){
              toast.error("Выбранная база данных не является допустимой");
            }
          }).catch(r => router.push({name: "dashboard"}))

      }

    return { data, submitForm, getDump, getBase64, submitRestoreDump}
  },

  async mounted(){
    const toast = useToast();
    const router = useRouter();

    await fetch(`/api/db/data`, {
      method: "GET",
      headers: {'Content-Type': "application/json"},
      credentials: "include",
    }).then(async response => {
        const result = await response.json();
        if(result.message == "OK"){
          this.data.server = result.data.server;
          this.data.database = result.data.database;
          this.data.user = result.data.user;
          this.data.password = result.data.password;
          this.data.port = result.data.port;
        }else{
          toast.error("Произошла ошибка при получении текущих данных БД");
        }
      }).catch(r => router.push({name: "dashboard"}))
  }
});
</script>