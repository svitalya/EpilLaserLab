<template>
<div>
  <div class ="flex-column">
    <h1 class="row h3">
      Добавить запись расписания
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
        <router-link tag="button" :to="{name: 'dashboard.schedules'}" type="submit" class="btn btn-secondary" style="width: 120px;">Назад</router-link>
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
  import UserStore from "@/components/Pages/Utils/UserStore";

export default defineComponent({
  components: {DataTable, TableBody, TableHead},
  async beforeCreate(){

    var user = await UserStore.user();
    var branchId = null;
    if(user.roleId == 2){
        branchId = user.admin.branchId
    }

    await fetch(`/api/masters?limit=9999&branchId=${branchId}`, {
      headers: {'Content-Type': "application/json"},
        credentials: "include"
    }).then(async responce => {
      let responceJson = await responce.json();
      if(responceJson.message == "OK"){
        this.masters = responceJson.data.recs;
        this.data.masterId = responceJson.data.recs[0].masterId;
      }
    });

    this.$forceUpdate();
  },
  setup(){
    const router = useRouter();
    const toast = useToast();

    const masters = reactive([{
      masterId:0, fio: ""
    }]);

    const data = reactive({
      date: new Date().toJSON().slice(0,10),
      masterId: null
    });
    const tableData = ref([])

    const submitForm = async (e) => {

      await fetch(`/api/schedules`, {
        method: "POST",
        headers: {'Content-Type': "application/json"},
        credentials: "include",
        body: JSON.stringify(data)
      }).then(async response => {
          const result = await response.json();
          if(result.message == "OK"){

            toast.success("Запись успешно добавлена");
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

    return {data, submitForm, tableData, masters}
  }
});
</script>