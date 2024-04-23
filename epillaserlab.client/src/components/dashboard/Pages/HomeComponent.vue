<template>
<h1 class="h2">
    Главная
</h1>
<div >
        <data-table :rows="tableData"
              :pagination="pagination"
              :sort="sort"
              striped
              @loadData="loadData"
              style="font-size: large !important;"
              >
        <template #thead>
            <table-head sortable="branch" @sorting="loadData">Филлиал</table-head>
            <table-head sortable="master" @sorting="loadData">Мастер</table-head>
            <table-head sortable="client" @sorting="loadData">Клиент</table-head>
            <table-head sortable="dateTime" @sorting="loadData">Дата и время</table-head>
            <table-head sortable="dateTime" @sorting="loadData">Услуга</table-head>
            <table-head sortable="dateTime" @sorting="loadData">Цена</table-head>
            <table-head style="width: 7%">
                <router-link
                    :to="{name: 'dashboard.applications.add'}"
                    tag="button" type="button" class="btn btn-success" >Добавить</router-link>
            </table-head>
        </template>

        <template #tbody="{row}">
            <table-body v-text="row.branch"/>
            <table-body v-text="row.master"/>
            <table-body v-text="row.client"/>
            <table-body v-text="row.dateTime"/>
            <table-body v-text="row.service"/>
            <table-body v-text="row.price"/>
            <table-body :data-id="row.applicationId" style="width: 7%">
                <button type="button" class="btn btn-primary me-2" @click="editClick">Открыть</button>
            </table-body>
        </template>
        <template #empty>
                <TableBodyCell class="d-flex justify-content-center" colspan="2">
                    Нет записей
                </TableBodyCell>
            </template>
    </data-table>
    </div>
</template>

<script lang="ts">
  import {
      defineComponent,
      ref,
  }            from "vue";

  import { useRouter } from "vue-router";
  import { useToast } from "vue-toastification";

  import DataTable from "../../../lib/DataTable/DataTable.vue";
  import { TableBody, TableHead } from "@jobinsjp/vue3-datatable"
  import "@jobinsjp/vue3-datatable/dist/style.css";
  export default defineComponent({
      components: { TableBody, TableHead, DataTable },

      setup() {

        const router = useRouter();

        const toast = useToast();


          const tableData = ref([])

          const pagination = ref({})

          const sort = ref({})

          const getId = (e) => e.target.parentNode.dataset.id;

          const editClick = async (e) => {
            let id = getId(e);
            await router.push({name: "dashboard.applications.edit", params: {id: id}});
          }

          const loadData = async (query) => {
            let page, limit, sortDirection, order;

            sortDirection = sort.value["sort"]

            if(typeof(query) == typeof("")){
                page = pagination.value["page"];
                limit = pagination.value["limit"];
                
                sortDirection = sortDirection == 'asc' ? 'desc' : 'asc';
                
                order = query.split(':')[0];

                if(order !== sort.value["order"]){
                    sortDirection = 'desc';
                }

            }else if(typeof(query) == typeof({})){
                page = (query.page - 1) < 0 ? 0 : query.page - 1;
                limit = query.per_page;
                order = sort.value["order"];
            }else{
                page = pagination.value["page"];
                limit = pagination.value["limit"];
                order = sort.value["order"];
            }

            let params = {
                    page: page,
                    limit: limit,
                    sort: sortDirection,
                    order: order
            }

            var prms = new URLSearchParams(params);

            await fetch(`https://localhost:7243/api/applications?${prms}`, {
                headers: {'Content-Type': "application/json"},
                credentials: "include"
            }).then(async responce => {
                let responceJson = await responce.json()

                if(responceJson.message = "OK"){
                    let data = responceJson.data;
                    let max = data.max;
                    page = data.page;
                    let recs = data.recs;

                    tableData.value = recs
                    pagination.value = { ...pagination.value, page: page, total: max, limit: limit}
                    sort.value = { ...sort.value, sort: sortDirection, order: order};
                }else{
                    tableData.value = [];
                    pagination.value = { ...pagination.value, page: 0, total: 0, limit: limit}
                    sort.value = { ...sort.value, sort: sortDirection, order: order};
                }
            }).catch(r => router.push({name: "dashboard"}))
          }


          return { tableData, pagination, loadData, sort, editClick}
      },
  })
</script>