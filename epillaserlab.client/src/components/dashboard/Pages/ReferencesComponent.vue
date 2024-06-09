<template>
  <div class ="flex-column">
    <h1 class="row h2">
      Справочники
  </h1>

    <div class="row list-group" v-for="reference in referencesData">
      <router-link style="width: 120px;" :to="'/dashboard/reference/'+reference.name" class="col-3 list-group-item list-group-item-action mt-2">{{reference.title}}</router-link>
    </div>
  </div>

</template>
  
<script>
import { defineComponent } from 'vue';
export default defineComponent({
  data(){
    return {
      referencesData: [{title: "", name: ""}]
    }
  },

  created(){
    fetch(`/api/dashboard/references`, {
      method: "GET",
      credentials: "include"
    }).then(result => result.json().then(j => this.referencesData = j.recs))
  }
})
</script>