<template>
  <div>
    <div class ="flex-column">
      <h1 class="row h3">
        Изменить запись услуги
      </h1>
    </div>
  
    <form @submit.prevent="submitForm" class="row mt-3">
      <div class = "col-3">
        <div class="row form-group mt-3">
          <label class="h5">Услуга</label>
          <div class="col-12">
            <select class="form-control" v-model="data.serviceId">
              <option v-for="service in serviceData" :value="service.serviceId">{{service.name}}</option>
            </select>
          </div>
        </div>
        <div class="row form-group mt-3">
          <label class="h5">Посещений</label>
          <div class="col-12">
            <input type="number" v-model="data.sessions" class="form-control" placeholder="Введите количество посещений">
          </div>
        </div>
        <div class="row form-group mt-3">
          <label class="h5">Дней действует</label>
          <div class="col-12">
            <input type="number" v-model="data.validityPeriod" class="form-control" placeholder="Введите количество дней">
          </div>
        </div>
        <div class="row form-group mt-3">
          <label class="h5">Цена</label>
          <div class="col-12">
            <input type="number" v-model="data.price" class="form-control" placeholder="Введите цену">
          </div>
        </div>
  
        <div class="row mt-4">
          <button type="submit" class="btn btn-success me-3" style="width: 120px;">Сохранить</button>
          <router-link tag="button" :to="{name: 'dashboard.seasontickets'}" type="submit" class="btn btn-secondary" style="width: 120px;">Назад</router-link>
        </div>
      </div>
    </form>
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


      await fetch("https://localhost:7243/api/services?"
        +"page=0&limit=999&order=name&sort=asc", {
          headers: {'Content-Type': "application/json"},
          credentials: "include",
        }).then(async responce => {
          let responceJson = await responce.json();
          if(responceJson.message == "OK"){
            
            this.serviceData = responceJson.data.recs;
            
            
          }
      });

      await fetch(`https://localhost:7243/api/seasontickets/${this.id}`, {
          headers: {'Content-Type': "application/json"},
          credentials: "include",
        }).then(async responce => {
          let responceJson = await responce.json();
          
          if(responceJson.message == "OK"){
            let rec = responceJson.rec;
            this.data.serviceId = rec.serviceId;
            this.data.sessions = rec.sessions;
            this.data.validityPeriod = rec.validityPeriod;
            this.data.price = rec.price;
          }
      });

      this.$forceUpdate();
    },
  
    setup(){
      const router = useRouter();
      const id = router.currentRoute.value.params.id;

      const toast = useToast();
  
      const data = reactive({
        serviceId: null,
        sessions: null,
        validityPeriod: null,
        price: null
      });
  
      let serviceData = reactive([{serviceId: 0, name: ""}]);
  
  
  
      const submitForm = async (e) => {
        await fetch(`https://localhost:7243/api/seasontickets/${id}`, {
          method: "PUT",
          headers: {'Content-Type': "application/json"},
          credentials: "include",
          body: JSON.stringify(data)
        }).then(async response => {
            const result = await response.json();
            if(result.message == "OK"){
  
              toast.success("Запись успешно добавлена");
              router.push({name: "dashboard.seasontickets"})
  
            }else if(result.message == "DUPLICATION"){
              toast.error("Дублирование записи");
            }else if(result.message == "NOT CHANGED"){
              toast.info("Внесите изменения");
            }else if(result.message == "DATA NOT VALID" || result.status == 400){
              toast.error("Введите значения");
            }else{
              toast.error("Ошибка при изменении записи");
            }
          }).catch(r => router.push({name: "dashboard"}))
      }; 
  
      return {data, submitForm, serviceData, id}
    }
  });
  </script>