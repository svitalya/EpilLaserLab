<template>
<div>
  <div class ="flex-column">
    <h1 class="row h3">
      Изменить купленный абонимент
    </h1>
  </div>

  <form @submit.prevent="submitForm" class="row mt-3">
    <div class="row form-group mt-3">
      <label class="h5">Клиент</label>
      <div class="col-8" v-if="clientSelected.val">
        <select class="form-control" v-model="data.clientId">
          <option v-for="client in clientsData" :value="client.clientId">{{client.name}}</option>
        </select>
      </div>
      <div class="col-8" v-else>
        <div class="row">
          <label class="h5">Введите имя</label>
          <div class="col-12">
            <input type="text" v-model="data.client.name" class="form-control"/>
          </div>
        </div>
        <div class="row">
          <label class="h5">Введите телефон</label>
          <div class="col-12">
            <input type="text" v-model="data.client.phone" class="form-control"/>
          </div>
        </div>

      </div>
      <div class="col-4">
        <button type="button" class="btn btn-secondary" @click="plusMinusBtnClickHandler">
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus" viewBox="0 0 16 16">
            <path v-if="clientSelected.val" d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z"></path>
            <path v-else d="M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8"></path>
          </svg>
        </button>
      </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Абонемент</label>
        <div class="col-8">
          <select class="form-control" v-model="data.seasonTicketPriceId">
            <option v-for="seasonTicket in seasonTickets" :value="seasonTicket.seasonTicketPriceId">{{seasonTicket.name}}</option>
          </select>
        </div>
    </div>

    <div class="row mt-4">
      <button type="submit" class="btn btn-success me-3" style="width: 120px;">Сохранить</button>
      <router-link tag="button" :to="{name: 'dashboard.purchased-season-tickets'}" type="submit" class="btn btn-secondary" style="width: 120px;">Назад</router-link>
    </div>
  </form>


</div>
</template>
<script lang="ts">
import { defineComponent, reactive } from 'vue';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';

export default defineComponent({
  async mounted(){
    await fetch(`/api/clients?limit=1000`, {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let recs = (await responce.json()).data.recs;
      
      this.clientsData = recs;
    });

    await fetch('/api/prices/subsciptions-list', {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let recs = (await responce.json()).recs;
      this.seasonTickets = recs;
    })

    fetch(`/api/purchasedseasontickets/${this.id}`, {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let rec = (await responce.json()).rec;
      this.data.clientId = rec.clientId;
      this.data.seasonTicketPriceId = rec.seasonTicketPriceId;
    });
  },
  setup(){
  
    const toast = useToast();
    const router = useRouter();

    const id = router.currentRoute.value.params.id;

    const clientsData = reactive([{clientId: 0, name: null}]);

    const clientSelected = reactive({val: true});
    const plusMinusBtnClickHandler = () => clientSelected.val = !clientSelected.val;

    const data = reactive({
      clientId: null,
      client: {
        name: null,
        phone: null
      },
      seasonTicketPriceId: null
    })

    const seasonTickets = reactive([{seasonTicketPriceId: 0, name: ""}])

    const submitForm = async (e) => {
        let poastData = {client: {name: data.client.name, phone: data.client.phone}, clientId: data.clientId, seasonTicketPriceId: data.seasonTicketPriceId};
        if(!poastData.client.name || !poastData.client.phone){
          poastData.client = null;
        }
        await fetch(`/api/purchasedseasontickets/${id}`, {
          method: "PUT",
          headers: {'Content-Type': "application/json"},
          credentials: "include",
          body: JSON.stringify(poastData)
        }).then(async response => {
            const result = await response.json();
            if(result.message == "OK"){
  
              toast.success("Запись успешно изменина");
              router.push({name: "dashboard.purchased-season-tickets"})
  
            }else if(result.message == "DUPLICATION"){
              toast.error("Дублирование записи");
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

    return {clientSelected, plusMinusBtnClickHandler, clientsData, data, seasonTickets, submitForm, id}
  }

})
</script>