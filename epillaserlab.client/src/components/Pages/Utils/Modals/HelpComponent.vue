<template>
  <div id="helperModal" >
    <div :class="{'simple-modal': true, 'd-none': hidden.value}" :ref="el => modalElement =(el as HTMLElement)">
      <div class="modal-container">
        <svg @click="closeCLick" src="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 30 30" fill="none" class="close-svg">
          <path d="M17.6759 15L29.8039 2.87256C30.0649 2.61119 30.0649 2.1877 29.8039 1.92633L28.0742 0.195701C27.9487 0.0705488 27.7781 -0.000244141 27.6008 -0.000244141C27.4232 -0.000244141 27.2528 0.0705488 27.1274 0.195701L14.9996 12.3234L2.87193 0.195701C2.62068 -0.0555511 2.17601 -0.0552351 1.92539 0.195701L0.196024 1.92633C-0.0653412 2.1877 -0.0653412 2.61119 0.196024 2.87256L12.3237 15L0.196024 27.1273C-0.0653412 27.3887 -0.0653412 27.8122 0.196024 28.0736L1.92571 29.8042C2.05118 29.9294 2.22152 30.0002 2.39914 30.0002C2.57675 30.0002 2.74678 29.9294 2.87225 29.8042L15 17.6765L27.1277 29.8042C27.2531 29.9294 27.4238 30.0002 27.6011 30.0002C27.7784 30.0002 27.9491 29.9294 28.0745 29.8042L29.8042 28.0736C30.0653 27.8122 30.0653 27.3887 29.8042 27.1273L17.6759 15Z" fill="#B0B0B0"/>
        </svg>
        <div class="simple-modal-content">
          <slot></slot>
        </div>
      </div>
      </div>
</div>
</template>
<script lang="ts">
import { defineComponent, reactive, ref} from 'vue';
import shortcut from '../../../../lib/shortcut.js'; 

export default defineComponent({
  props:{
    _hidden: {type: Boolean, default: true},
  }, 

  setup(props){
    const hidden = reactive({value: props._hidden});
    const scroll = reactive({value: 0})
    const modalElement = ref();

    shortcut.add("F1", function() {
      hidden.value = !hidden.value;
      scroll.value = window.scrollY;
      modalElement.value.style.top = `${window.scrollY}px`

    });

    return {hidden, scroll, modalElement}
  },
  methods:{
    closeCLick(){
      this.hidden.value = true
    }
  }
})
</script>
<style>
#helperModal{
  position: absolute;
  z-index: 9999;

  *, ::after, ::before {
    box-sizing: content-box;
  }

  .simple-modal {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    display: flex;
    align-items: start;
    justify-content: center;
    background-color: rgba(#000, 0.25);

    padding-top: 50px;
    margin: 0;

    width: 100vw;
    height: 100vh;

    &.d-none{
      display: none;
    }

    z-index: 999;

  }

  .modal-container {
    height: auto;

    max-height: 80vh;
    width: 1140px;

    background-color: #4D396A;
    overflow-y: scroll;

    margin-left: auto;
    margin-right: auto;
    
    overflow: hidden;
    display: flex;
    flex-direction: column;
    align-items: end;

    box-shadow: 0 15px 30px 0 rgba(#000, 0.5);

    border-radius: 10px;

    padding-top: 65px;
    padding-bottom: 65px;
  }

  .close-svg{
    margin-right: 50px;
    margin-left: 50px;

    cursor: pointer;

    path{
      fill: #B0B0B0
    }
  }

  .close-svg:hover{
    path{
      fill: #D9D9D9
    }
  }

  .simple-modal-content{

    display: flex;
    flex-direction: column;
    align-items: start;
    justify-content: start;

    padding-right: 25px;
    padding-left: 25px;

    height: 70vh;
    width: 90%;

    overflow-y: scroll;
    margin-right: 55px;
    margin-top: 15px;

    h1, h2, h3, h4, p{
      text-align: start;
      justify-content: start;
      color: white;
    }
  }
}
</style>