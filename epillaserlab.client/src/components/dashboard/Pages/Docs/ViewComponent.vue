<template>
<div>
  <button type="button" class="btn btn-primary" @click="docButtonClick" data-type="returnability">Возвращаемость</button> 
  <button type="button" class="btn btn-primary" @click="docButtonClick" data-type="returnability">Продажи по сотрудникам</button> 
  <button type="button" class="btn btn-primary" @click="docButtonClick" data-type="returnability">Продажи по услугам</button> 
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