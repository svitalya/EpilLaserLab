<template>
  <div>
    <div class ="flex-column">
      <h1 class="row h3">
        Редактировать запись расписания
      </h1>
    </div>
  
    <form @submit.prevent="submitForm" class="row mt-3">
      <div class = "col-3">
        <div class="row form-group mt-3">
          <label class="h5">Дата</label>
          <div class="col-12">
            <input type="date" v-model="data.date" class="form-control" placeholder="Введите название" :min="new Date().toJSON().slice(0,10)">
          </div>
        </div>
  
        <div class="row form-group mt-3">
          <label class="h5">Мастер</label>
          <div class="col-12">
            <select class="form-control" v-model="data.masterId">
              <option v-for="master in masters" :value="master.masterId">{{master.fio}}</option>
            </select>
          </div>
        </div>

        <div class="row mt-4">
          <button type="submit" class="btn btn-success me-3" style="width: 120px;">Сохранить</button>
        </div>
      </div>
  
    </form>

    <div class="row form-group mt-5 col-6">
      <label class="h5">Рабочие интервалы</label>
      <div class="col-12">
        <textarea type="text" v-model="intervals.intervalString" class="form-control" placeholder="Введите интервал"/>
      </div>
      <div class="row mt-4">
          <button type="button" class="btn btn-primary me-3" style="width: 120px;" v-on:click="setIntervals">Установить</button>
          <router-link tag="button" :to="{name: 'dashboard.schedules'}" type="submit" class="btn btn-secondary" style="width: 120px;">Назад</router-link>
        </div>
    </div>
  </div>
  
  </template>
  <script lang="ts">
  import { reactive, ref } from 'vue';
  import { useRouter } from 'vue-router';
  import { useToast } from "vue-toastification";
  import { defineComponent } from 'vue';
  
  import DataTable from "../../../../lib/DataTable/DataTable.vue";
    import { TableBody, TableHead } from "@jobinsjp/vue3-datatable"
    import "@jobinsjp/vue3-datatable/dist/style.css";
  
  export default defineComponent({
    components: {DataTable, TableBody, TableHead},
    async beforeCreate(){
      await fetch("https://localhost:7243/api/masters?limit=9999", {
        headers: {'Content-Type': "application/json"},
          credentials: "include"
      }).then(async responce => {
        let responceJson = await responce.json();
        if(responceJson.message == "OK"){
          this.masters = responceJson.data.recs;
        }
      });

      await fetch(`https://localhost:7243/api/schedules/${this.id}`, {
        headers: {'Content-Type': "application/json"},
          credentials: "include"
      }).then(async responce => {
        let responceJson = await responce.json();
        if(responceJson.message == "OK"){
          this.data.date = responceJson.rec.date.replace('T00:00:00', '');
          this.data.masterId = responceJson.rec.masterId;
        }
      });

      await fetch(`https://localhost:7243/api/intervals/${this.id}`, {
        headers: {'Content-Type': "application/json"},
          credentials: "include"
      }).then(async responce => {
        let responceJson = await responce.json();
        if(responceJson.message == "OK"){
          this.intervals.intervalString = responceJson.intervalsString;
        }
      });
  
      this.$forceUpdate();
    },
    setup(){
      const router = useRouter();

      const id = router.currentRoute.value.params.id;

      const toast = useToast();
  
      const masters = reactive([{
        masterId:0, fio: ""
      }]);

      const intervals = reactive({
        intervalString: ""
      })
  
      const data = reactive({
        date: new Date().toJSON().slice(0,10),
        masterId: null
      });

      const setIntervals = async(e) => {
        await fetch(`https://localhost:7243/api/intervals/${id}`, {
          method: "POST",
          headers: {'Content-Type': "application/json"},
          credentials: "include",
          body: `"${intervals.intervalString}"`
        }).then(async response => {
            const result = await response.json();
            if(result.message == "OK"){
  
              toast.success("Запись успешно изменена");
  
            }else if(result.message == "DUPLICATION"){
                toast.error("Дублирование записи");
              }else if(result.message == "NOT CHANGED"){
                toast.info("Внесите изменения");
              }else if(result.message == "DATA NOT VALID" || result.status == 400){
                toast.error("Введите значения");
              }else{
                toast.error("Ошибка при добавлении записи");
              }
          }).catch(r => console.log(r))
      }
  
      const submitForm = async (e) => {
  
        await fetch(`https://localhost:7243/api/schedules/${id}`, {
          method: "PUT",
          headers: {'Content-Type': "application/json"},
          credentials: "include",
          body: JSON.stringify(data)
        }).then(async response => {
            const result = await response.json();
            if(result.message == "OK"){
  
              toast.success("Запись успешно изменена");
              router.push({name: "dashboard.schedules"})
  
            }else if(result.message == "DUPLICATION"){
                toast.error("Дублирование записи");
              }else if(result.message == "NOT CHANGED"){
                toast.info("Внесите изменения");
              }else if(result.message == "DATA NOT VALID" || result.status == 400){
                toast.error("Введите значения");
              }else{
                toast.error("Ошибка при добавлении записи");
              }
          }).catch(r => router.push({name: "dashboard"}))
      }; 
  
      return {data, submitForm,masters, intervals, id, setIntervals}
    }
  });
  </script>