<template>
<div>
  <div class ="flex-column">
    <h1 class="row h3">
      Добавить запись заявки
    </h1>
  </div>

  <form @submit.prevent="clickBtn">

    <div class="row form-group mt-3">
      <label class="h5">Клиент</label>
      <div class="col-12">
        <select class="form-control" v-model="data.clientId">
          <option v-for="client in clientsData" :value="client.clientId">{{client.name}}</option>
        </select>
      </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Услуга</label>
      <div class="col-12">
        <select class="form-control" v-model="data.serviceId" @change="changeService">
          <option v-for="service in servicesData" :value="service.serviceId" >{{service.name}} - {{service.timeCost}}мин.</option>
        </select>
      </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Прайс лист</label>
      <div class="col-12">
        <select class="form-control" v-model="data.typeId">
          <option v-for="typePrice in typesData" :value="typePrice.id">{{typePrice.name}}</option>
        </select>
      </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Категория</label>
      <div class="col-12">
        <select class="form-control" v-model="data.categoryId">
          <option v-for="category in categories" :value="category.id">{{category.name}}</option>
        </select>
      </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Филиал</label>
      <div class="col-12">
        <select class="form-control" v-model="data.branchId" @change="branchChange">
          <option v-for="branch in branches" :value="branch.branchId">{{branch.address}}</option>
        </select>
      </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Мастер</label>
      <div class="col-12">
        <select class="form-control" v-model="data.masterId"  @change="changeMaster">
          <option v-for="master in masters" :value="master.masterId">{{master.fio}}</option>
        </select>
      </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Дни работы</label>
      <div class="col-12">
        <select class="form-control" v-model="data.scheduleId" @change="changeSchedule">
          <option v-for="schedul in scheduls" :value="schedul.scheduleId">{{schedul.dateString}}</option>
        </select>
      </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Доступное время</label>
        <div class="col-12">
          <textarea type="text" :value="intervals.intervalString" class="form-control" disabled/>
        </div>
    </div>

    <div class="row form-group mt-3">
      <label class="h5">Введите время</label>
        <div class="col-12">
          <input type="text" v-model="data.timeStart" class="form-control"/>
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
import { useToast } from 'vue-toastification';

export default defineComponent({
  async beforeCreate() {
    fetch("https://localhost:7243/api/clients?limit=1000", {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let recs = (await responce.json()).data.recs;
      this.clientsData = recs;
      this.data.clientId = recs[0].clientId
    });

    fetch("https://localhost:7243/api/services?limit=1000", {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let recs = (await responce.json()).data.recs;
      this.servicesData = recs;
      this.data.serviceId = recs[0].serviceId
      this.timeCost = await this.getTimeCostService();
    });

    fetch("https://localhost:7243/api/types?limit=1000", {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let recs = (await responce.json()).data.recs;
      this.typesData = recs;
      this.data.typeId = recs[0].id  
    });

    fetch("https://localhost:7243/api/categories?limit=1000", {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let recs = (await responce.json()).data.recs;
      this.categories = recs;
      this.data.categoryId = recs[0].id  
    });


   fetch("https://localhost:7243/api/branches?limit=1000", {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async responce => {
      let recs = (await responce.json()).data.recs;
      this.branches = recs;
      this.data.branchId = recs[0].branchId  

      await this.branchChange();
      this.$forceUpdate();
    })
  },
  setup(){
    const instance = getCurrentInstance();

    const toast = useToast();
    

    const clientsData = reactive([{clientId: 0, name: null}]);
    const servicesData = reactive([{serviceId: 0, name: null}]); // typesData
    const typesData = reactive([{id: 0, name: null}]);
    const categories = reactive([{id: 0, name: null}]);
    const branches = reactive([{branchId: 0, address: null}]);
    let masters = ref([{masterId: null, fio: ""}]);
    let scheduls = ref([{scheduleId: null, dateString: ""}]);
    let intervals = ref({intervalString: ""});
    let timeCost = 0;
    
    const data = reactive({
      clientId: null,
      serviceId: null,
      typeId: null,
      categoryId: null,
      branchId: null,
      masterId: null,
      scheduleId: null,
      timeStart: null
    })

    const branchChange = async() => {
      fetch(`https://localhost:7243/api/masters?limit=1000&branchId=${(data.branchId ?? -1)}`, {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
      }).then(async responce => {
        let recs = (await responce.json()).data.recs;
        

        if(recs.length > 0){
          masters.value = recs;
          data.masterId = recs[0].masterId ?? null;
        }else{
          masters.value = [];
          data.masterId = null;
        }

        await changeMaster()
        
      })
    };

    const changeMaster = async() => {
      fetch(`https://localhost:7243/api/schedules?limit=1000&masterId=${(data.masterId ?? -1)}`, {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
      }).then(async responce => {
        let recs = (await responce.json()).data.recs;
        if(recs.length > 0){
          scheduls.value = recs;
          data.scheduleId = recs[0].scheduleId ?? null;
        }else{
          scheduls.value = [];
          data.scheduleId = null;
        }

        timeCost = await getTimeCostService();

        await changeSchedule()
      })
    }

    const changeSchedule = async() => {
      fetch(`https://localhost:7243/api/intervals/${data.scheduleId ?? -1}?timeCost=${timeCost}`, {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
      }).then(async responce => {
        intervals.value.intervalString = (await responce.json()).intervalsString ?? "Нет графика";
      })
    }

    const getTimeCostService = async () =>{
      let servicesData = [];
      await fetch("https://localhost:7243/api/services?limit=1000", {
        method: "GET",
        credentials: "include",
        headers: {'Content-Type': "application/json"}
      }).then(async responce => {
        let recs = (await responce.json()).data.recs;
        servicesData = recs;
      });

      for(let service of servicesData){
        if(service.serviceId == data.serviceId){
          return service.timeCost;
        }
      }
    }

    const clickBtn = async () => {
      await fetch(`https://localhost:7243/api/applications`, {
        method: "POST",
        credentials: "include",
        headers: {'Content-Type': "application/json"},
        body: JSON.stringify(data)
      }).then(async responce => {
          console.log(data);
          toast.success("ПППП");
      })
    };

    const changeService = async() =>{
      timeCost = await getTimeCostService();
      console.log(timeCost);
      await changeSchedule()
    }

    return {data,
      clientsData,
      servicesData,
      typesData,
      categories,
      branches,
      masters,
      branchChange,
      changeMaster,
      scheduls,
      intervals,
      changeSchedule,
      timeCost,
      getTimeCostService,
      changeService,
      clickBtn
    }
  }
});
</script>
