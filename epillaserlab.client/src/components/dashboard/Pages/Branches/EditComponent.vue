<template>
  <div>
    <div class ="flex-column">
      <h1 class="row h3">
        Изменить запись филиала
      </h1>
    </div>
  
    <form @submit.prevent="submitForm" class="row mt-3">
      <div class = "col-3">
        <div class="row form-group mt-3">
          <img width = "300px" height = "300px" :src="dataImg.imgSrc" id="image">
          <label class="h5">Изображение</label>
          <div class="input-group mb-3">
            <input type="file" class="form-control" v-on:change="e => {if (e.target.files.length > 0) getBase64(e.target)}">
            <label class="input-group-text">Загрузка</label>
          </div>
        </div>
        <div class="row form-group mt-3">
          <label class="h5">Адрес</label>
          <div class="col-12">
            <input type="text" v-model="data.address" class="form-control" placeholder="Введите адрес">
          </div>
        </div>
        <div class="row mt-4">
          <button type="submit" class="btn btn-success me-3" style="width: 120px;">Сохранить</button>
          <router-link tag="button" :to="{name: 'dashboard.branches'}" type="submit" class="btn btn-secondary" style="width: 120px;">Назад</router-link>
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
  
  export default defineComponent({
    components: {DataTable, TableBody, TableHead},
    
    async beforeCreate(){
      await fetch(`/api/branches/${this.id}`, {
          method: "GET",
          headers: {'Content-Type': "application/json"},
          credentials: "include",
      }).then(async responce => {
        let responceJson = await responce.json();

        if(responceJson.message != 'OK'){
          this.toast.error("Ошибка при получении записи");
          return;
        }

        let rec = responceJson.rec;
        this.dataImg.imgSrc = `/resources/images/${rec.photoPath}`;
        this.data.address = rec.address;
      });

      this.$forceUpdate();
    },
    setup(){
  
      const router = useRouter();

      const id = router.currentRoute.value.params.id;

      const toast = useToast();

      const dataImg = reactive({imgSrc: "https://imgholder.ru/300X300/8493a8/adb9ca&font=kelson"});
  
      const data = reactive({
        address: null,
        photoFile: null
      });
  
      const getBase64 = (target) => {
        let file = target.files[0]; 
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function () {
          let base64Data = (<String>reader.result).split(";");
          let format = base64Data[0];
  
          if(["data:image/jpeg", "data:image/png", "data:image/jpg"].indexOf(format) != -1){
            data.photoFile = base64Data[1].replace('base64,', '');
            (<HTMLImageElement>document.getElementById("image")).src = (<string>reader.result);
          }else{
            target.value = null;
            toast.error("Недопустимый тип файла");
          }
        };
        reader.onerror = function (error) {
          console.log('Error: ', error);
        };
      }
  
  
      const submitForm = async (e) => {
        await fetch(`/api/branches/${id}`, {
          method: "PUT",
          headers: {'Content-Type': "application/json"},
          credentials: "include",
          body: JSON.stringify(data)
        }).then(async response => {
            const result = await response.json();

            
            if(result.message == "OK"){
              toast.success("Запись успешно изменена");
              router.push({name: "dashboard.branches"})
            }else if(result.message == "DUPLICATION"){
              toast.error("Дублирование записи");
            }else if(result.message == "NOT CHANGED"){
              toast.info("Внесите изменения");
            }else if(result.message == "DATA NOT VALID" || result.status == 400){
              toast.error("Введите значения");
            }else{
              toast.error("Ошибка при изменении записи");
            }
          }).catch(r => router.push({name: "dashboard"}))
      }; 
  
      return {data, submitForm, getBase64, id, dataImg}
    }
  });
  </script>