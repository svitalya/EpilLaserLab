<template>
 <create-applications 
  :price-id="priceId"
  :time-cost="timeCost"
  :hidden="modalHidden.val"
  @closeModal="closeModalHandler"
  :key="JSON.stringify(modalHidden)"/>
  <div class="price-content" id="zonesOneTimeSessions">
      <div class="zone-one-time-sessions-content" v-for="price in prices" @click.stop="clickPrice" :data-id="price.id" :data-time-cost="price.timeCost">
        <div class="zone-one-time-sessions-content-header" :data-id="price.id" :data-time-cost="price.timeCost">{{price.header}}</div>
        <div class="zone-one-time-sessions-content-desciption" :data-id="price.id" :data-time-cost="price.timeCost">
          <div class="zone-one-time-sessions-content-desciption-text" :data-id="price.id" :data-time-cost="price.timeCost">
            {{price.description}}
          </div>
          <div class="zone-one-time-sessions-content-desciption-line" :data-id="price.id" :data-time-cost="price.timeCost">
          </div>
          <div class="zone-one-time-sessions-content-desciption-price" :data-id="price.id" :data-time-cost="price.timeCost">
            {{price.price}}
          </div>
        </div>
      </div>    
  </div>
</template>
<script lang="ts">
import { defineComponent, reactive } from 'vue';
import CreateApplicationsComponent from '../Modals/CreateApplicationsComponent.vue';

interface ZoneOneTimeSessionsContent{
  id: number,
  header: string,
  description: string,
  price: number,
  timeCost: number
}

export default defineComponent({
  components:{
    "create-applications": CreateApplicationsComponent
  },
  props:{
    prices: {type: Array<ZoneOneTimeSessionsContent>, required:true}
  },
  data(){
    return {
      priceId: null,
      timeCost: null,
    }
  },
  setup(){
    const modalHidden = reactive({val: true});
    return {modalHidden}
  },
  methods:{
    clickPrice(e: Event){
      this.timeCost = (e.target as HTMLElement).dataset.timeCost;
      this.priceId = (e.target as HTMLElement).dataset.id; 
      this.modalHidden.val = false; 
    },

    closeModalHandler(){
      this.modalHidden.val = true; 
    }
  }
})
</script>

<style>
.price-content{
  &#zonesOneTimeSessions{
        background-color: #7D6A9A;
        display: flex;
        flex-direction: column;
        overflow:auto;
        overflow-x:hidden;
        padding: 10px;
        padding-top: 0px;
        align-items: center;
        flex-wrap: nowrap;

        .zone-one-time-sessions-content{
          height: auto;
          width: calc(100% - 20px);
          border-radius: 10px;
          margin-top: 10px;
          margin-bottom: auto;
          cursor: pointer;

          padding: 10px;

          display: flex;
          flex-direction: column;
          flex-wrap: wrap;
          align-items: start;



          &:hover{
            background-color: #B4A9C4;
          };
        
          .zone-one-time-sessions-content-header{
            text-align: start;
            letter-spacing: 5%;
            font-size: 28px;
            font-weight: 600;
            color: white;
            line-height: auto;
            width: 100%;
          }

          .zone-one-time-sessions-content-desciption{
            width: 100%;
            flex: 1;

            display: flex;
            flex-direction: row;
            align-items: end;
            flex-wrap: wrap;

            .zone-one-time-sessions-content-desciption-text{
              color: white;
              height: 100%;
              display: inline;
              flex-direction: row;
              align-items: start;

              margin-top: 0px;
              font-size: 16px;
              letter-spacing: 5%;
              line-height: 110%;
              font-weight: 400;
              text-transform: lowercase;
              padding-bottom: 3px;

              flex: 1;
            }

            .zone-one-time-sessions-content-desciption-line{
              flex: 3;
              margin-bottom: 3px;
              border-bottom: 2px solid white;
            }

            .zone-one-time-sessions-content-desciption-price{
              font-size: 28px;
              letter-spacing: 5%;
              line-height: auto;
              font-weight: 600;
              text-transform: uppercase;
              color: #F7EFB2;
              margin-right: 25px;
              margin-left: 10px;
            }
          }
        }
    }
}


</style>