<template>
<div class="row justify-content-between">
  <button type="button" class="btn btn-primary col-3 me-3" @click="docButtonClick" data-type="returnability">Возвращаемость</button> 
  <button type="button" class="btn btn-primary col-3 me-3" @click="docButtonClick" data-type="salesbyemployees">Продажи по сотрудникам</button> 
  <button type="button" class="btn btn-primary col-3 me-3" @click="docButtonClick" data-type="salesbyservices">Продажи по услугам</button>
</div>

</template>

<script>
import {defineComponent} from "vue"

export default defineComponent({
  setup(){
    const docButtonClick = (e) => {
      let type = e.target.dataset.type;

      fetch(`/api/document/${type}`, {
        method: "POST",
        credentials: "include",
        headers: {'Content-Type': "application/json"}
      }).then(async r => {
        let jsonResult = await r.json();
        window.open(jsonResult.fileUrl, "_blank")
      });
    }

    return {docButtonClick}
  }
})
</script>