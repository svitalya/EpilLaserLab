<template>
  <create-applications/>
  <div class="price-zone">
    <div class="price-header">{{header}}</div>
    <div class="price-subheader">{{ subheader }}</div>
    <slot></slot>
  </div>
</template>
<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  props:{
    header: {type: String, required: true},
    subheader: {type: String, required: true}
  }
  
});
</script>
<style>
.price-zone{
  width: 1100px;
  height: 568px; 
  border-radius: 10px;
  background-color: #4D396A;
  display: flex;
  flex-direction: column;
  align-items: center;

  padding: 0 20px 0;

  .price-header{
    margin-top: 30px;
    font-size: 28px;
    letter-spacing: 5%;
    line-height: auto;
    font-weight: 600;
    color: white;
    text-transform: uppercase;
  }

  .price-subheader{
    margin-top: 0px;
    font-size: 44px;
    letter-spacing: 5%;
    line-height: 114%;
    font-weight: bold;
    color: #F7EFB2;
    text-transform: uppercase;
  }
  .price-content{
    width: 100%;
    height: 100%;
    margin: 30px 0px 30px;
    
    border-radius: 20px;
  }
}
</style>