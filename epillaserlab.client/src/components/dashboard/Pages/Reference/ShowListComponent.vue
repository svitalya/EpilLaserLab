<template>
  <data-table :rows="tableData"
              :pagination="pagination"
              :sort="sort"
              filter
              striped
              @loadData="loadData"
              style="font-size: large !important;"
              >
      <template #thead>
          <table-head sortable="name" @sorting="loadData" style="width: 70%;">Название</table-head>
          <table-head>
            <router-link
                :to="{name: 'dashboard.reference.add', params: {referencename: refName}}"
                tag="button" type="button" class="btn btn-success" >Добавить</router-link>
        </table-head>
      </template>

      <template #tbody="{row}">
        <table-body v-text="row.name" style="width: 70%"/>
        <table-body :data-id="row.id">
            <button type="button" class="btn btn-primary me-2" @click="editClick">ИЗМЕНИТЬ</button>
            <button type="button" class="btn btn-danger" @click="delClick">УДАЛИТЬ</button>
        </table-body>
      </template>
      <template #empty>
            <TableBodyCell colspan="2">
                Нет записей
            </TableBodyCell>
        </template>
  </data-table>
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
            const refName = router.currentRoute.value.params.referencename

            const toast = useToast();


          const tableData = ref([])

          const pagination = ref({})
          const search = ref({})

          const sort = ref({})

          const getId = (e) => e.target.parentNode.dataset.id;

          const editClick = async (e) => {
            let id = getId(e);
            await router.push({name: "dashboard.reference.edit", params: { referencename: refName, id: id }});
          }

          const delClick = async (e) => {
            let isDel = confirm("Вы точно хотите удалить запись?");

            if(!isDel) return;

            let id = getId(e);
            const responce = await fetch(`https://localhost:7243/api/${refName}/${id}`, {
                method: "DELETE",
                credentials: "include"
            })
            const data = await responce.json();

            if(data.message == 'OK'){
                toast.success("Запись удалена");
                loadData(undefined)
            }else if(data.message == 'BLOCK'){
                toast.error("Удаление записи приведет к потере данных");
            }else{
                toast.error("Неизвестная ошибка при удалении записи");
            }
            
          }

          const loadData = async (query) => {
        

            let page, limit, sortDirection, searchInfo;

            sortDirection = sort.value["sort"]

            if(typeof(query) == typeof("")){
                page = pagination.value["page"];
                limit = pagination.value["limit"];
                searchInfo = search.value['search']
                
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
                searchInfo = search.value['search']
            }

            let params = {
                    page: page,
                    limit: limit,
                    sort: sortDirection,
                    search: searchInfo
            }

            var prms = new URLSearchParams(params);

            const responce = await fetch(`https://localhost:7243/api/${refName}?${prms}`, {
                headers: {'Content-Type': "application/json"},
                credentials: "include"
            })

            let data = await responce.json()
            let max = data.max;
            page = data.page;
            let recs = data.data;

            tableData.value = recs
            pagination.value = { ...pagination.value, page: page, total: max, limit: limit}
            sort.value = { ...sort.value, sort: sortDirection};
            search.value = {...search.value, search: searchInfo};
          }


          return { tableData, pagination, loadData, sort, delClick, refName, editClick}
      },
  })

  export default Paginated
</script>
<style>
</style>