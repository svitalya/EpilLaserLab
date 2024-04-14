<template>
<form @submit.prevent="submitForm">
  <div class="form-group">
    <label>Изменить запись</label>
    <input type="text" v-model="data.name" class="form-control mt-3" placeholder="Введите значение">
  </div>
  <div class="row mt-3">
    <button type="submit" class="btn btn-success me-3" style="width: 120px;">Сохранить</button>
    <router-link tag="button" :to="{name: 'dashboard.reference', params: {referencename: refName}}" type="submit" class="btn btn-secondary" style="width: 120px;">Назад</router-link>
  </div>
</form>
</template>
<script lang="ts">
import { reactive } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from "vue-toastification";
import { defineComponent } from 'vue';
export default defineComponent({
  async beforeCreate() {
    const response = await fetch(`https://localhost:7243/api/${this.refName}/${this.idRec}`, {
        method: "GET",
        headers: {'Content-Type': "application/json"},
        credentials: "include"
    });

    const result = await response.json();
    this.data.name = result.name;
    this.oldDataName.name = result.name;
  },
  setup(){

    let oldDataName = reactive({name: ""}); 
    const router = useRouter();
    const refName = router.currentRoute.value.params.referencename;
    const idRec = router.currentRoute.value.params.id;

    const toast = useToast();

    const data = reactive({
      name: "",
    });


    const submitForm = async (e) => {
      if(data.name == ""){
        return toast.info("Введите значение");
      }

      if(oldDataName.name == data.name){
        return toast.info("Введите новое значение");
      }

      await fetch(`https://localhost:7243/api/${refName}/${idRec}`, {
        method: "PUT",
        headers: {'Content-Type': "application/json"},
        credentials: "include",
        body: JSON.stringify(data)
      }).then(async responce => {
        const result = await responce.json();

        if(result.message == "OK"){
          toast.success("Запись успешно изменена");
          router.push({name: "dashboard.reference", params: {referencename: refName}})
        }else if(result.message == "DUPLICATION"){
          toast.error("Дублирование записи");
        }else{
          toast.error("Ошибка при изменении записи");
        }
      }).catch(e => router.push({name: "dashboard"}))
    }; 
    return {data, submitForm, refName, idRec, oldDataName}
  }
});
</script>