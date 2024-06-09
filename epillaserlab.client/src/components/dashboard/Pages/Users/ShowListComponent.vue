<template>
  <div>
    <data-table :rows="tableData" :pagination="pagination" :sort="sort" striped @loadData="loadData">
      <template #thead>
        <table-head sortable="login" @sorting="loadData">Логин</table-head>
        <table-head sortable="email" @sorting="loadData">Email</table-head>
        <table-head sortable="role" @sorting="loadData" v-if="!role">Роль</table-head>
        <table-head v-if="role == 'admin'">
          <router-link
                    :to="{name: 'dashboard.users.admins.reg'}"
                    tag="button" type="button" class="btn btn-success" >Добавить</router-link>
        </table-head>
      </template>

      <template #tbody="{row}">
        <table-body v-text="row.login"/>
        <table-body v-text="row.email"/>
        <table-body v-if="!role" v-text="row.role"/>
      </template>

      <template #empty>
        <TableBodyCell class="d-flex justify-content-center" colspan="2">
          Нет записей
        </TableBodyCell>
      </template>
    </data-table>
  </div>
</template>

<script>
import { defineComponent, ref, getCurrentInstance} from "vue";
import { watch } from 'vue'

import { useRouter, useRoute} from "vue-router";
  import { useToast } from "vue-toastification";

  import DataTable from "../../../../lib/DataTable/DataTable.vue";
  import { TableBody, TableHead } from "@jobinsjp/vue3-datatable"
  import "@jobinsjp/vue3-datatable/dist/style.css";

export default defineComponent({
  components: { DataTable, TableHead, TableBody },
  setup() {
    const router = useRouter();
    const route = useRoute();

    const role = ref(route.params.role ?? null);

    watch(() => route.params.role, (newRole) => {
      role.value = newRole ?? false;
      loadData(); // or whatever initial query you want to send
    });

    const tableData = ref([]);
    const pagination = ref({});
    const sort = ref({});

    const loadData = async (query) => {
      let page, limit, sortDirection, order;

      if (typeof query === "string") {
        page = pagination.value.page;
        limit = pagination.value.limit;
        sortDirection = sort.value.sort === "asc" ? "desc" : "asc";
        order = query.split(":")[0];
      } else if (typeof query === "object") {
        page = query.page - 1 < 0 ? 0 : query.page - 1;
        limit = query.per_page;
        order = sort.value.order;
      } else {
        page = pagination.value.page;
        limit = pagination.value.limit;
        order = sort.value.order;
      }

      let params = {
        page: page,
        limit: limit,
        sort: sortDirection,
        order: order,
      };

      if(role.value){
        params.role = role.value;
      }

      var prms = new URLSearchParams(params);

      await fetch(`/api/auth/users?${prms}`, {
        headers: { "Content-Type": "application/json" },
        credentials: "include",
      })
        .then(async (response) => {
          let responseJson = await response.json();

          if (responseJson.message === "OK") {
            let data = responseJson.data;
            let max = data.max;
            page = data.page;
            let recs = data.recs;

            tableData.value = recs;
            pagination.value = { ...pagination.value, page: page, total: max, limit: limit };
            sort.value = { ...sort.value, sort: sortDirection, order: order };
          } else {
            tableData.value = [];
            pagination.value = { ...pagination.value, page: 0, total: 0, limit: limit };
            sort.value = { ...sort.value, sort: sortDirection, order: order };
          }
        })
        .catch((r) => console.error(r));
    };

    return { tableData, pagination, sort, loadData, role};
  },
});
</script>