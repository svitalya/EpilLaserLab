<template>
<form class="row col-3" @submit.prevent="submitForm">
  <div class="form-group">
    <label>Добавить запись</label>
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
  setup(){
    const router = useRouter();
    const refName = router.currentRoute.value.params.referencename

    const toast = useToast();

    const data = reactive({
      name: "",
    });

    const submitForm = async (e) => {
      if(data.name == ""){
        return toast.info("Введите значение");
      }

      await fetch(`https://localhost:7243/api/${refName}`, {
        method: "POST",
        headers: {'Content-Type': "application/json"},
        credentials: "include",
        body: JSON.stringify(data)
      }).then(async response => {
          const result = await response.json();
          if(result.message == "OK"){
            toast.success("Запись успешно добавлена");
            router.push({name: "dashboard.reference", params: {referencename: refName}})
          }else if(result.message == "DUPLICATION"){
            toast.error("Дублирование записи");
          }else{
            toast.error("Ошибка при добавлении записи");
          }
        }).catch(r => router.push({name: "dashboard"}))
    }; 

    return {data, submitForm, refName}
  }
});
</script>