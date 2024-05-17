<template>
<div>
  <div class="content">
    <men
      :show="showMenu"
      pageId="price"
      :vertical="true"
      @shortCloseClick="$emit('shortCloseClick', $event)"
      @scrollToComponent="$emit('scrollToComponent', $event)"/>


    <div id="priceContent">
      <div id="priceListContent"> 
        <price-select @selectPriceList="selectPriceListHandler"/>
      </div>
      <div id="priceZoneContent">
        <price-zone :header="header" :subheader="subheader">
          <component :is="selectedPriceList" :prices="prices"></component>
        </price-zone>
      </div>
    </div>

  </div>
</div>
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import  MenuComponent  from '../MenuComponent.vue';
import PriceSelectContainerComponen from './Utils/PriceSelect/PriceSelectContainerComponen.vue';
import PriceZoneComponent from './Utils/Prices/PriceZoneComponent.vue';
import ZonesOneTimeSessionsComponent from './Utils/Prices/ZonesOneTimeSessionsComponent.vue';
import SubscriptionPricesComponent from './Utils/Prices/SubscriptionPricesComponent.vue';

export default defineComponent({
  props:{
    showMenu: {type: Boolean, default: false}
  },
  data(){
    return {
      selectedPriceList: defineComponent({}),
      prices: [],
      header: "Выберите категорию",
      subheader: ""
    }
  },
  components:{
    "men": MenuComponent,
    "price-select": PriceSelectContainerComponen,
    "price-zone": PriceZoneComponent,
    "zone-one-times": ZonesOneTimeSessionsComponent,
    "subscription-prices": SubscriptionPricesComponent
  },

  methods:{
    async selectPriceListHandler(priceList: string){

      try{
        this.prices = (await (await fetch(`https://localhost:7243/api/prices/${priceList}`)).json()).recs;
      }catch{
        return;
      }
      
      if(priceList === "subsciptions"){
        this.selectedPriceList = SubscriptionPricesComponent;
        this.header = ""
        this.subheader = "цены абонементов"
      } 
      else if(priceList === "acquaintance"){
        this.selectedPriceList = ZonesOneTimeSessionsComponent;
        this.header = "знакомство"
        this.subheader = "Разовые услуги"
      }
      else if(priceList === "standard"){
        this.selectedPriceList = ZonesOneTimeSessionsComponent;
        this.header = "стандартный"
        this.subheader = "Разовые услуги"
      }
      else{
        this.selectedPriceList = defineComponent({}),
        this.prices = [],
        this.header = "Выберите категорию",
        this.subheader = ""
      }
    }
  }
})
</script>
<style>
#price{
  background-color: #8A8099;
  position: relative;
}

#price .content{
  width: 1200px;
  height: 700px;
  top: calc(100% / 2);
  left: calc(100% / 2);
  display: flex;
  flex-direction: row;

  #priceContent{
    display: flex;
    flex-direction: column;
    margin-left: 25px;

    #priceListContent{
      margin-top: 0;
    }
  }
}

</style>