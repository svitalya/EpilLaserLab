<template>
<div>
  <div class="content">
    <men
      :show="showMenu"
      pageId="cabinet"
      :vertical="true"
      :isClient="isClient"
      @shortCloseClick="$emit('shortCloseClick', $event)"
      @scrollToComponent="$emit('scrollToComponent', $event)"
      @logOut="$emit('logOut', $event)"
      />

      <div id="cabinet-content">
        <div class="header">{{list.length > 0 ? 'Ваши записи' : 'Вы еще не записаны'}}</div>
        <div class="app-list">
          <div class="app-row" v-for="app in list" :key="JSON.stringify(app)">{{`${app.service} - ${app.branch} - ${app.master} - ${app.dateTime}`}}</div>
        </div>
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
    showMenu: {type: Boolean, default: false},
    isClient: {type: Boolean, default: false},
    user: {type: Object, default: {}}
  },

  data(){
    return {
      list: [{service: "", branch: "", master: "", dateTime: ""}]
    }
  },

  setup(){

  },

  mounted(){
    fetch(`api/applications/short?clientId=${this.user.client.clientId}`, {
      method: "GET",
      credentials: "include",
      headers: {'Content-Type': "application/json"}
    }).then(async r => {
      this.list = (await r.json()).data.recs
    });
  }
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

      padding: 20px 20px;

      margin-left: 20px;

      .header{
        color: #F7EFB2;
        width: 100%;
        font-weight: bold;
        font-size: 44px;
        line-height: 114%;
        letter-spacing: 5%;
        text-align: start;
        margin-bottom: 20px;
      }
      .app-list{
        width: 100%;
        height: 100%;
        padding: 5px;
        overflow-y: scroll;

        .app-row{
          padding: 15px 15px;
          width: 100%;
          background-color: #B4A9C4;
          border-radius: 5px;
          color: white;
          font-size: 28px;
        

          &:not(:last-child){
            margin-bottom: 15px;
          }
        }


      }

    }
  }
}
</style>