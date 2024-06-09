<template>
<data-table :rows="tableData"
            :pagination="pagination"
            :sort="sort"
            striped
            @loadData="loadData"
            style="font-size: large !important;"
            >
        <template #thead>
            <table-head sortable="name" @sorting="loadData">Услуга</table-head>
            <table-head sortable="client" @sorting="loadData">Клиент</table-head>
            <table-head sortable="remains" @sorting="loadData">Оставшиеся посещения</table-head>
            <table-head sortable="dateOfCombustion" @sorting="loadData">Дата сгорания</table-head>
            <table-head>
            <router-link
                :to="{name: 'dashboard.purchased-season-tickets.add'}"
                tag="button" type="button" class="btn btn-success">Добавить</router-link>
        </table-head>
        </template>

        <template #tbody="{row}">
        <table-body v-text="row.name"/>
        <table-body v-text="row.client"/>
        <table-body v-text="row.remains"/>
        <table-body v-text="row.dateOfCombustionString"/>
        <table-body :data-id="row.purchasedSeasonTicketId">
            <button type="button" class="btn btn-primary me-2" @click="editClick">ИЗМЕНИТЬ</button>
            <button type="button" class="btn btn-danger" @click="delClick">УДАЛИТЬ</button>
        </table-body>
        </template>
        <template #empty>
            <TableBodyCell class="d-flex justify-content-center" colspan="5">
                Нет записей
            </TableBodyCell>
        </template>
    </data-table>
</template>
<script lang="ts">
  import {ref, defineComponent}from "vue";

  import { useRouter } from "vue-router";
  import { useToast } from "vue-toastification";

  import DataTable from "../../../../lib/DataTable/DataTable.vue";
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
            await router.push({name: "dashboard.purchased-season-tickets.edit", params: {id: id}});
          }

          const delClick = async (e) => {
            let isDel = confirm("Вы точно хотите удалить запись?");

            if(!isDel) return;

            let id = getId(e);
            await fetch(`/api/purchasedseasontickets/${id}`, {
                method: "DELETE",
                credentials: "include"
            }).then(async responce => {
                const data = await responce.json();

                if(data.message == 'OK'){
                    toast.success("Запись удалена");
                    loadData(undefined)
                }else if(data.message == 'BLOCK'){
                    toast.error("Удаление записи приведет к потере данных");
                }else{
                    toast.error("Ошибка при удалении записи");
                }
            }).catch(r => router.push({name: "dashboard"})); 
          }

          const loadData = async (query) => {
        
            let page, limit, sortDirection, searchInfo;

            sortDirection = sort.value["sort"]

            if(typeof(query) == typeof("")){
                page = pagination.value["page"];
                limit = pagination.value["limit"];
                
                if(sortDirection == 'asc'){
                    sortDirection = 'desc';
                }else{
                    sortDirection = 'asc';
                }

            }else if(typeof(query) == typeof({})){
                page = (query.page - 1) < 0 ? 0 : query.page - 1;
                limit = query.per_page;
                searchInfo = query.search;
            }else{
                page = pagination.value["page"];
                limit = pagination.value["limit"];
            }

            let params = {
                    page: page,
                    limit: limit,
                    sort: sortDirection,
                    search: searchInfo
            }

            var prms = new URLSearchParams(params);
            await fetch(`/api/purchasedseasontickets?${prms}`, {
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
                    sort.value = { ...sort.value, sort: sortDirection};
                }else{
                    tableData.value = [];
                    pagination.value = { ...pagination.value, page: 0, total: 0, limit: limit}
                    sort.value = { ...sort.value, sort: sortDirection};
                }
            }).catch(r => router.push({name: "dashboard"}))
          }


          return { tableData, pagination, loadData, sort, delClick, editClick}
      },
})
</script>