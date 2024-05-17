<template>
  <div id="priceSelectContainer">

    <template v-for="priceSelect in pricesSelect">
      <price-select
        v-if="!!priceSelect"
        :id="priceSelect.id"
        :options="priceSelect.options"
        @selectChanged="selectChangedHandler"
      />
    </template>

  </div>
</template>

<!-- :options="[
{id: '1', value: 'Разовые услуги'},
{id: '2', value: 'Абонименты'},
]" -->
<script lang="ts">
import { DefineComponent, defineComponent, reactive, ref, Ref} from 'vue';
import PriceSelectComponent from './PriceSelectComponent.vue';

export default defineComponent({
  components:{
    "price-select": PriceSelectComponent
  },

  setup(){

    const pricesSelect = reactive([]);
    pricesSelect.push({
        id: "type",
        options: [
          {id: 'onetimes', value: 'Разовые услуги'},
          {id: 'subsciptions', value: 'Абонименты'},]
      });

    return {pricesSelect}
  },

  methods: {
    selectChangedHandler(selectedData){
      if(selectedData.selectId == "type" && selectedData.value == "onetimes" && this.pricesSelect.length <= 1){
        this.pricesSelect.push({
          id: "onetimesCategory",
          options: [
            {id: 'acquaintance', value: 'Знакомство'},
            {id: 'standard', value: 'Стандартный'}]
        })
      }else if(selectedData.selectId == "type" && selectedData.value == "subsciptions" && this.pricesSelect.length >= 2){
        delete this.pricesSelect[1];
        this.pricesSelect.pop();
      }

      this.$emit("selectPriceList",  selectedData.value);
    }
  }
})
</script>
<style>

#priceSelectContainer{
  width: 100%;
  height: 62px;
  margin-bottom: 15px;

  display: flex;
  flex-direction: row;

  .__select {
    position: relative;
    width: 250px;
    height: 62px;
    color: #F7EFB2;
    font-size: 16px;

    margin: 0;
    margin-right: 20px;

    background-color: #4D396A;
    border-radius: 10px;


    &[data-state="active"] {
      .__select__title {
        &::before {
          transform: translate(-3px, -50%) rotate(-45deg);
        }

        &::after {
          transform: translate(3px, -50%) rotate(45deg);
        }
      }

      .__select__content {
        opacity: 1;
      }

      .__select__label + .__select__input + .__select__label {
        max-height: 62px;
        border-top-width: 1px;
      }
    }
  }
  .__select__title {
    display: flex;
    align-items: center;
    width: 100%;
    height: 100%;
    padding: 8px 16px;

    cursor: pointer;

    &::before,
    &::after {
      content: "";

      position: absolute;
      top: 50%;
      right: 16px;

      display: block;
      width: 10px;
      height: 2px;

      transition: all 0.3s ease-out;

      background-color: #4D396A;

      transform: translate(-3px, -50%) rotate(45deg);
    }
    &::after {
      transform: translate(3px, -50%) rotate(-45deg);
    }

    &:hover {
      border-color: #F7EFB2;

      &::before,
      &::after {
        background-color: #F7EFB2;
      }
    }
  }
  .reset {
    display: flex;
    width: 250px;
    padding: 8px 16px;
    margin: 0 auto;
    margin-bottom: 10px;

    border: solid 10px #4D396A;
    border-radius: 5px;

    transition: all 0.2s ease-out;

    cursor: pointer;

    font-weight: bold;

    background-color: #4D396A;
    color: #4D396A;

    &:hover {
      background-color: #4D396A;
      color: #9C7638;
    }
  }
  .__select__content {
    position: absolute;
    top: 55px;

    display: flex;
    flex-direction: column;
    width: 100%;

    background-color: #4D396A;

    border: 1px solid #4D396A;
    border-top: none;
    border-bottom-left-radius: 5px;
    border-bottom-right-radius: 5px;

    transition: all 0.3s ease-out;

    opacity: 0;
    z-index: 8;
  }
  .__select__input {
    display: none;

    &:checked + label {
      background-color: #4D396A;
    }
    &:disabled + label {
      opacity: 0.6;
      pointer-events: none;
    }
  }
  .__select__label {
    display: flex;
    align-items: center;
    width: 100%;
    height: 52px;
    max-height: 0;
    padding: 0 16px;

    transition: all 0.2s ease-out;

    cursor: pointer;

    overflow: hidden;

    & + input + & {
      border-top: 0 solid #c7ccd160;
    }

    &:hover {
      background-color: #4D396A !important;

      color: #9C7638;
    }
  }
}
</style>