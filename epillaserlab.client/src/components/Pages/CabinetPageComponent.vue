<template>
<div>
  <div class="content">
    <men
      :show="showMenu"
      pageId="cabinet"
      :vertical="true"
      @shortCloseClick="$emit('shortCloseClick', $event)"
      @scrollToComponent="$emit('scrollToComponent', $event)"/>

      <div id="cabinet-content">

      </div>
  </div>
</div>  
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import MenuComponent from '../MenuComponent.vue';

export default defineComponent({
  components: {
    "men": MenuComponent,
  },
  props:{
    showMenu: {type: Boolean, default: false}
  },
})
</script>

<style>
#cabinet{
  background-color: #8A8099;
  position: relative;

  .content{
    width: 1200px;
    height: 700px;
    top: calc(100% / 2);
    left: calc(100% / 2);
    display: flex;
    flex-direction: row;

    #cabinet-content{
      width: 1100px;
      height: 568px; 
      border-radius: 10px;
      background-color: #4D396A;
      display: flex;
      flex-direction: column;
      align-items: center;

      padding: 0 20px 0;

      margin-left: 20px;
    }
  }
}
</style>