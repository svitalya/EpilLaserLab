<template>
<button type="button" class="btn btn-primary" @click="DocButtonClick">Документ</button> 
</template>

<script>
import {defineComponent} from "vue"

export default defineComponent({
  setup(){
    const DocButtonClick = () => {
      window.open(`/api/document/test`, '_blank');
    }

    return {DocButtonClick}
  }
})
</script>