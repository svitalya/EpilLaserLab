<template>
<div>
  <div class ="flex-column">
    <h1 class="row h3">
      Добавить запись услуги
    </h1>
  </div>

  <form @submit.prevent="submitForm" class="row mt-3">
    <div class = "col-3">
      <div class="row form-group mt-3">
        <label class="h5">Название</label>
        <div class="col-12">
          <input type="text" v-model="data.name" class="form-control" placeholder="Введите название">
        </div>
      </div>
      <div class="row form-group mt-3">
        <label class="h5">Описание</label>
        <div class="col-12">
          <textarea type="text" v-model="data.description" class="form-control" placeholder="Введите описание"/>
        </div>
      </div>
      <div class="row form-group mt-3">
        <label class="h5">Время выполнения</label>
        <div class="col-12">
          <input type="number" v-model="data.timeCost" class="form-control" placeholder="Введите врем">
        </div>
      </div>


      <div class="row mt-4">
        <button type="submit" class="btn btn-success me-3" style="width: 120px;">Сохранить</button>
        <router-link tag="button" :to="{name: 'dashboard.services'}" type="submit" class="btn btn-secondary" style="width: 120px;">Назад</router-link>
      </div>
    </div>

    <div class = "col-9">
      <data-table :rows="tableData"
              striped
              id = "pricesTable"
              @loadData="loadData"
              style="font-size: large !important;">
        <template #thead>
            <table-head>Вид прайслиста</table-head>
            <table-head>Цена</table-head>
            <table-head></table-head>
        </template>

        <template #tbody="{row}">
            <table-body v-text="row.name"/>
            <table-body><input class="form-control" id="service_price_val" :data-type-id="row.id" type="number"></table-body>
            <table-body></table-body>
        </template>
        <template #empty>
                <TableBodyCell class="d-flex justify-content-center" colspan="2">
                    Нет записей
                </TableBodyCell>
            </template>
    </data-table>
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
  setup(){
    const router = useRouter();
    const toast = useToast();
    const data = reactive({
      name: "",
      description: "",
      timeCost: 0,
      priceByType: []
    });
    const tableData = ref([])
    const loadData = async (query) => {
      await fetch(`/api/types`, {
          headers: {'Content-Type': "application/json"},
          credentials: "include"
      }).then(async responce => {
          let responceJson = await responce.json()

          if(responceJson.message == "OK"){
              tableData.value = responceJson.data.recs
          }else{
              toast.error("Ошибка");
          }

      }).catch(r => router.push({name: "dashboard"}))
    }

    const submitForm = async (e) => {
      data.priceByType = [];
      document.querySelectorAll("input#service_price_val")
        .forEach(item => {
          let i = (<HTMLInputElement>item);
          console.log(i);
          if(i.value){
            data.priceByType.push({key: i.dataset.typeId, value: Number(i.value)})
          }   
      });


      await fetch(`/api/services`, {
        method: "POST",
        headers: {'Content-Type': "application/json"},
        credentials: "include",
        body: JSON.stringify(data)
      }).then(async response => {
          const result = await response.json();
          if(result.message == "OK"){

            toast.success("Запись успешно добавлена");
            router.push({name: "dashboard.services"})

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

    return {data, submitForm, tableData, loadData}
  }
});
</script>