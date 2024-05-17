<template>
<div class="__select" data-state="" :ref="el => initSelec(el as HTMLElement)">
  <div class="__select__title" data-default="">Выберите</div>
  <div class="__select__content">

      <input id="singleSelect2" class="__select__input" type="radio" name="singleSelect"/>
      <label for="singleSelect2" class="__select__label">Ничего</label>

      <template v-for="option in options">
        <input :id="`${id}-${option.id}`" class="__select__input" type="radio" :name="id" :data-id="option.id"/>
        <label :for="`${id}-${option.id}`" class="__select__label" :data-id="option.id">{{ option.value }}</label>
      </template>
  </div>
</div>
</template>
<script lang="ts">
import {HtmlHTMLAttributes, defineComponent} from 'vue';

interface Option{
  id: string,
  value: string
}

export default defineComponent({
  props:{
    id: {type: String, required: true},
    options: {type: Array<Option>, required: true}
  },

  methods:{
    initSelec(el: HTMLElement){
      const selectSingle = el;

      if(!el) return;

      const selectSingle_title = selectSingle.querySelectorAll(".__select__title")[0] as HTMLElement;
      const selectSingle_labels = selectSingle.querySelectorAll('.__select__label') as NodeListOf<HTMLElement>;
      selectSingle_title.addEventListener('click', () => {
        if ('active' === selectSingle.getAttribute('data-state')) {
          selectSingle.setAttribute('data-state', '');
        } else {
          selectSingle.setAttribute('data-state', 'active');
        }
      });

      // Close when click to option
      for (let i = 0; i < selectSingle_labels.length; i++) {
        selectSingle_labels[i].addEventListener('click', (evt) => {
          selectSingle_title.textContent = (evt.target as HTMLElement).textContent;
          this.$emit("selectChanged", {selectId: this.id, value: (evt.target as HTMLElement).dataset.id});
          selectSingle.setAttribute('data-state', '');
        });
      }
    }
  }
})
</script>