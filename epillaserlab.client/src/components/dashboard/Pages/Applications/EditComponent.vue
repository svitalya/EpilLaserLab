<template>
<div>
  <div class ="flex-column">
    <h1 class="row h3">
      Закрыть заявку
    </h1>
  </div>

  <form @submit.prevent="clickBtn">

    <div class="row form-group mt-3">
      <label class="h5">Клиент: {{data.client}}</label>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Услуга: {{data.service}}</label>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Филиал: {{data.branch}}</label>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Мастер: {{data.master}}</label>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Дата: {{data.dateTime}}</label>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Категория</label>
      <div class="col-12">
        <select class="form-control" v-model="data.categoryId">
          <option v-for="category in categories" :value="category.id">{{category.name}}</option>
        </select>
      </div>
    </div>
  
  <div class="row mt-4">
    <button type="submit" class="btn btn-success me-3" style="width: 120px;">Сохранить</button>
  </div>

</form>

</div>
</template>
<script lang="ts">
import { getCurrentInstance, defineComponent, reactive, ref } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

export default defineComponent({
  async beforeCreate() {
    await fetch(`/api/categories?limit=1000`, {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let recs = (await responce.json()).data.recs;
      this.categories = recs;
      this.data.categoryId = recs[0].id  
    });

    await fetch(`/api/applications/${this.id}/short`, {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      this.data = (await responce.json()).data;
    });
  },
  setup(){
    const instance = getCurrentInstance();
    const toast = useToast();
    const router = useRouter();
    const id = router.currentRoute.value.params.id;
    
    const categories = reactive([{id: 0, name: null}]);
    
    const data = ref({
      client: "",
      service: "",
      branch: "",
      master: "",
      dateTime: "",
      categoryId:0
    })

    const clickBtn = async () => {
      await fetch(`/api/applications/${id}/category`, {
        method: "PUT",
        credentials: "include",
        headers: {'Content-Type': "application/json"},
        body: data.value.categoryId.toString()
      }).then(async responce => {
        var request = await(responce.json())

        if(request.message == "OK"){
          toast.success("Заявка успешно закрыта");
          router.push({name: "dashboard.home"});
        }
      })
    };

    return {data,
      categories,
      clickBtn,
      id
    }
  }
});
</script>
