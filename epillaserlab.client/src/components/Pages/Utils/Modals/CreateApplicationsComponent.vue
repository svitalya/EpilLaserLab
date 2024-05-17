<template>
  <div id="createAppModal" >
    <div :class="{'simple-modal': true, 'd-none': hidden}">
    <div class="modal-container">
      <svg @click="closeCLick" :ref="el => modalEl = (el as HTMLElement)" src="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 30 30" fill="none" class="close-svg">
        <path d="M17.6759 15L29.8039 2.87256C30.0649 2.61119 30.0649 2.1877 29.8039 1.92633L28.0742 0.195701C27.9487 0.0705488 27.7781 -0.000244141 27.6008 -0.000244141C27.4232 -0.000244141 27.2528 0.0705488 27.1274 0.195701L14.9996 12.3234L2.87193 0.195701C2.62068 -0.0555511 2.17601 -0.0552351 1.92539 0.195701L0.196024 1.92633C-0.0653412 2.1877 -0.0653412 2.61119 0.196024 2.87256L12.3237 15L0.196024 27.1273C-0.0653412 27.3887 -0.0653412 27.8122 0.196024 28.0736L1.92571 29.8042C2.05118 29.9294 2.22152 30.0002 2.39914 30.0002C2.57675 30.0002 2.74678 29.9294 2.87225 29.8042L15 17.6765L27.1277 29.8042C27.2531 29.9294 27.4238 30.0002 27.6011 30.0002C27.7784 30.0002 27.9491 29.9294 28.0745 29.8042L29.8042 28.0736C30.0653 27.8122 30.0653 27.3887 29.8042 27.1273L17.6759 15Z" fill="#B0B0B0"/>
      </svg>
      <div class="simple-modal-content">
          <div class="modal-row">
            <span class="row-header">Адрес</span>
            <div class="select-box" :ref="el => !!el ? initSelectBox(el as HTMLElement, selectBranch) : undefined">
              <div :class="{'select-item': true, 'active': index==0}" v-for="(branch, index) in branches" :key="branch.branchId" :data-id="branch.branchId">
                <div class="image-container"><img class="image" :src="`https://localhost:7243/resources/images/${branch.photoPath}`"></div>
                <div class="text">{{branch.address}}</div> 
              </div>
            </div>
          </div>     
          <div class="modal-row">
            <span class="row-header">Мастер</span>
            <div class="select-box" :ref="el => !!el ? initSelectBox(el as HTMLElement, selectMaster) : undefined">
              <div :class="{'select-item': true, 'active': index==0}" v-for="(master, index) in masters" :key="master.masterId" :data-id="master.masterId">
                <div class="image-container"><img class="image" :src="`https://localhost:7243/resources/images/${master.photoPath}`"></div>
                <div class="text">{{master.fio}}</div> 
              </div>
            </div>
          </div> 
          <div class="modal-row">
            <span class="row-header">Дата</span>
            <div class="select-box" :ref="el => !!el ? initSelectBox(el as HTMLElement, selectSchedule) : undefined">
              <div :class="{'select-item': true, 'minimize':true, 'active': index==0}" v-for="(schedul, index) in scheduls" :key="schedul.scheduleId" :data-id="schedul.scheduleId">
                <div class="text center">{{schedul.dateShortString}}</div>
              </div>
            </div>
          </div>
          <div class="modal-row">
            <span class="row-header">Время</span>
            <div class="select-box" :ref="el => !!el ? initSelectBox(el as HTMLElement, selectIntervale, false) : undefined">
              <div :class="{'select-item': true, 'minimize':true, 'active': index==0}" v-for="(interval, index) in intervals" :key="interval" :data-id="interval">
                <div class="text center">{{ interval }}</div>
              </div>
            </div>
          </div>
          <div class="modal-row row-direction">
             <div class="input-container">
                <span class="label">Имя</span> 
                <input type="text" v-model="data.client.name">
             </div>
             <div class="input-container">
              <span class="label">Телефон</span> 
              <input type="text" v-model="data.client.phone">
           </div>
          </div>
          <div class="modal-row flex-start">
            <button class="send-button" @click="clickButtonHandler">Отправить</button>
          </div>
      </div>

    </div>
  </div>
  </div>

</template>
<script lang="ts">
import { defineComponent } from 'vue';
import { useToast } from 'vue-toastification';

interface SelectChangedCallbackFunction {
    (valId: number): void;
}

interface SelectChangedCallbackFunctionWithString {
    (valId: string): void;
}

export default defineComponent({
  props:{
    hidden: {type: Boolean, default: true},
    timeCost: {type: Number, required: true},
    priceId: {type: Number, required: true},
  },

  data(){
    return {
      modalEl: {},
      data:{
        priceId: this.priceId,
        branchId: null,
        masterId: null,
        scheduleId: null,
        timeStart: "",

        client:{
          phone: "",
          name: "",
        }

      },
      branches: [],
      masters: [],
      scheduls: [],
      intervals: []
    }
  },

  mounted(){
    this.getBranches();
  },
  methods: {
    getBranches(){
      fetch("https://localhost:7243/api/branches?limit=1000", {
        method: "GET",
        credentials: "include",
        headers: {'Content-Type': "application/json"}
      }).then(async responce => {
        let recs = (await responce.json()).data.recs;
        this.branches = recs;
        this.data.branchId = recs[0].branchId  

        await this.getMasters();
      })
    },

    selectBranch(branchId: number){
      this.data.branchId = branchId;
      this.getMasters();
    },

    async getMasters(){
      if(!!this.data.branchId){
        await fetch(`https://localhost:7243/api/masters?limit=1000&branchId=${(this.data.branchId ?? -1)}`, {
          method: "GET",
          credentials: "include",
          headers: {'Content-Type': "application/json"}
          }).then(async responce => {
            let recs = (await responce.json()).data.recs;
            

            if(recs.length > 0){
              this.masters = recs;
              this.data.masterId = recs[0].masterId ?? null;
            }else{
              this.masters = [];
              this.data.masterId = null;
            }     
          })
      }else{
        this.masters = [];
        this.data.masterId = null;
      }

      await this.getSchedules();
    },

    selectMaster(masterId: number){
      this.data.masterId = masterId;
      this.getSchedules();
    },

    async getSchedules(){
      if(!!this.data.masterId){

        await fetch(`https://localhost:7243/api/schedules?limit=1000&masterId=${(this.data.masterId ?? -1)}`, {
          method: "GET",
          credentials: "include",
          headers: {'Content-Type': "application/json"}
        }).then(async responce => {
          let recs = (await responce.json()).data.recs;
          if(recs.length > 0){
            this.scheduls = recs;
            this.data.scheduleId = recs[0].scheduleId ?? null;
          }else{
            this.scheduls = [];
            this.data.scheduleId = null;
          }
        })
      }else{
        this.scheduls = [];
        this.data.scheduleId = null;
      }

      await this.getTimeIntervals();
    },

    selectSchedule(scheduleId: number){
      this.data.scheduleId = scheduleId;
      this.getTimeIntervals();
    },

    async getTimeIntervals(){
      if(!!this.data.scheduleId){
        await fetch(`https://localhost:7243/api/intervals/${this.data.scheduleId}/separate?timeCost=${(this.timeCost)}`, {
          method: "GET",
          credentials: "include",
          headers: {'Content-Type': "application/json"}
        }).then(async responce => {
          let recs = (await responce.json()).recs;
          if(recs.length > 0){
            this.intervals = recs;
            this.data.timeStart = recs[0] ?? null;
          }else{
            this.intervals = [];
            this.data.timeStart = null;
          }
        })
      }else{
        this.intervals = [];
        this.data.timeStart = null;
      }
    },

    selectIntervale(intervaleString: string){
      this.data.timeStart = intervaleString;
    },

    closeCLick(){
      this.$emit('closeModal');
    },

    async clickButtonHandler(){
      await fetch(`https://localhost:7243/api/applications/`, {
          method: "POST",
          credentials: "include",
          headers: {'Content-Type': "application/json"},
          body: JSON.stringify(this.data)

        }).then(async responce => {
          let toast = useToast();
          if((await responce.json()).message == 'OK'){
            toast.success("Регистрация новой заявки прошло успешно");
            this.$emit('closeModal');
          }else{
            toast.success("Ошибка при регистрации заявки");
          }
      })
    },


    initSelectBox(el: HTMLElement, callback: SelectChangedCallbackFunction|SelectChangedCallbackFunctionWithString = null, valIsNum:Boolean = true){
      var items = el.querySelectorAll('.select-item');

      const selected = async (e) => {
        let localEl = e.target as HTMLElement;

        while(!localEl.classList.contains("select-item")){
          localEl = localEl.parentElement;
        }

        el.querySelector('.select-item.active').classList.remove('active');
        localEl.classList.add('active');

        if(callback != null && localEl.dataset.id){
          let val = localEl.dataset.id

          if(valIsNum){
              await (callback as SelectChangedCallbackFunction)(Number(val))
          }else{
            await (callback as SelectChangedCallbackFunctionWithString)(val)
          }  
          
        }

        e.stopPropagation();
        e.preventDefault();
      }

      items.forEach(i => i.addEventListener('click', selected))
    }
  }
})
</script>
<style>

#createAppModal{
  *, ::after, ::before {
    box-sizing: content-box;
  }

  .simple-modal {
	position: absolute;
	top: 0;
	left: 0;
	right: 0;
	bottom: 0;
	display: flex;
	align-items: start;
	justify-content: center;
	background-color: rgba(#000, 0.25);

  padding-top: 50px;
  margin: 0;

  width: 100vw;
  height: 100vh;

  &.d-none{
    display: none;
  }

}

.modal-container {
	height: auto;

  max-height: 80vh;
	width: 1140px;

  background-color: #4D396A;
  overflow-y: scroll;

	margin-left: auto;
	margin-right: auto;
	
	overflow: hidden;
	display: flex;
	flex-direction: column;
  align-items: end;

	box-shadow: 0 15px 30px 0 rgba(#000, 0.5);

  border-radius: 10px;

  padding-top: 65px;
  padding-bottom: 65px;
}

.close-svg{
  margin-right: 50px;
  margin-left: 50px;

  cursor: pointer;

  path{
    fill: #B0B0B0
  }
}

.close-svg:hover{
  path{
    fill: #D9D9D9
  }
}

.simple-modal-content{

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: start;

  padding-right: 25px;
  padding-left: 25px;

  height: 70vh;
  width: 90%;

  overflow-y: scroll;
  margin-right: 55px;
  margin-top: 15px;
}

.modal-row{
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;
  height: auto;
  margin-bottom: 30px;

  &.row-direction{
    flex-direction: row;
    flex-wrap: wrap;
  }

  &.flex-start{
    align-items: start;
  }

  .send-button{
    border-radius: 10px;
    padding: 10px 15px;
    background-color: #7D6A9A;
    outline-width: 0;
    border: none;

    font-weight: 500;
    font-size: 24px;
    text-align: center;
    justify-content: center;
    color: white;

    cursor: pointer;

    &:hover{
      color: #F7EFB2;
    }
  }

  .input-container{
    flex: 1;

    display: flex;
    flex-direction: column;

    padding: 15px 40px;

    .label{
      font-weight: 500;
      font-size: 28px;
      line-height: auto;
      letter-spacing: 5%;
      text-align: start;
      justify-content: center;
      color: white;
      margin-bottom: 10px;
    }
    
    input{
      min-height: 30px;
      border-radius: 10px;
      border: 5px solid #7D6A9A;
      font-size:24px;
      padding: 3px;
    }

    input:focus{
      outline-width: 0;
      border: 5px solid #F7EFB2;
    }
  }

  .row-header{
    color: white;
    line-height: 114%;
    letter-spacing: 5%;
    font-weight: bold;
    font-size: 44px;

    margin-bottom: 21px;
  }

  .select-box{
    display: flex;
    flex-direction: row;

    overflow-x: scroll;
    white-space: nowrap;
    flex-wrap: nowrap;

    width: 100%;
    height: auto;

    margin-bottom: 25px;

    .select-item{
      width: 195px;
      height: 222px;

      &.minimize{
        height: auto;
      }

      background-color: #7D6A9A;
      border-radius: 10px;
      padding: 15px;
      margin-bottom: 10px;
      border: 5px solid #7D6A9A;

      cursor: pointer;
      
      margin-right: 20px;

      display: flex;
      flex-direction: column;

      .image-container{
        display: flex;
        flex: 2;

        margin-bottom: 10px;

        img.image{
          width: 200px;
          height: 151px;
          border-radius: 10px;
        }
      }

      &.active, &:hover{
        border: 5px solid #F7EFB2;
      }

      .text{
        flex: 1;
        display: flex;

        font-weight: 400;
        font-size: 24px;
        line-height: auto;
        letter-spacing: 0;
        text-align: start;
        justify-content: start;
        color: white;
        white-space: normal;

        &.center{
          text-align: center;
          justify-content: center;
        }
      }
      

      &:last-child{
        margin-right: 0;
      }
    }

  }
}
}


</style>