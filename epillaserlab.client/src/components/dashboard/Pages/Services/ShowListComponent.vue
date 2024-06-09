<template>
    <div >
        <data-table :rows="tableData"
              :pagination="pagination"
              :sort="sort"
              striped

              @loadData="loadData"
              style="font-size: large !important;"
              >
        <template #thead>
            <table-head sortable="name" @sorting="loadData">Название</table-head>
            <table-head sortable="description" @sorting="loadData">Описание</table-head>
            <table-head sortable="timeCost" @sorting="loadData">Время выполнения</table-head>
            <table-head style="width: 15%">
                <router-link
                    :to="{name: 'dashboard.service.add'}"
                    tag="button" type="button" class="btn btn-success" >Добавить</router-link>
            </table-head>
        </template>

        <template #tbody="{row}">
            <table-body v-text="row.name"/>
            <table-body v-text="row.description"/>
            <table-body v-text="row.timeCost"/>
            <table-body :data-id="row.serviceId" style="width: 15%">
                <button type="button" class="btn btn-primary me-2" @click="editClick">ИЗМЕНИТЬ</button>
                <button type="button" class="btn btn-danger" @click="delClick">УДАЛИТЬ</button>
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

  import DataTable from "../../../../lib/DataTable/DataTable.vue";
  import { TableBody, TableHead } from "@jobinsjp/vue3-datatable"
  import "@jobinsjp/vue3-datatable/dist/style.css";
  const Paginated = defineComponent({
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
            await router.push({name: "dashboard.service.edit", params: {id: id}});
          }

          const delClick = async (e) => {
            let isDel = confirm("Вы точно хотите удалить запись?");

            if(!isDel) return;

            let id = getId(e);

            await fetch(`/api/services/${id}`, {
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

            await fetch(`/api/services?${prms}`, {
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


          return { tableData, pagination, loadData, sort, delClick, editClick}
      },
  })

  export default Paginated
</script>
<style>
</style>