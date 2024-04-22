<template>
<button type="button" class="btn btn-primary" @click="testDocButtonClick">Документ</button> 
</template>

<script>
import {defineComponent} from "vue"

export default defineComponent({
  setup(){
    const testDocButtonClick = () => {
      window.open("https://localhost:7243/api/document/test", '_blank');
    }

    return {testDocButtonClick}
  }
})
</script>