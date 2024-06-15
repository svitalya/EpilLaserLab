// FullScreenSlider.vue
<template>
  <help>
    <h1>Помощь</h1>

    <h2>Пример 1</h2>
    <p>Тест тест тест тест тест тест</p>
  </help>
  <div class="full-screen-slider">
    <component
      @scrollToComponent="handleScrollToComponent"
      @shortCloseClick="shortCloseClickHandler"
      @logOut="logOutHandler"
      v-for="componentData in components"
      :is="componentData.component"
      :id="componentData.id"
      :user="user"
      :show-menu="showMenu"
      :key="JSON.stringify(componentData)+isClient"
      :isClient="isClient"
      class="full-screen-component"></component>
  </div>
</template>

<script lang="ts">
import { ref, onMounted, Ref, reactive} from 'vue'
import { watch } from 'vue'
import MainPageComponent  from './Pages/MainPageComponent.vue'
import PricePageComponent  from './Pages/PricePageComponent.vue'
import CabinetPageComponent  from './Pages/CabinetPageComponent.vue'
import HelpComponent from './Pages/Utils/Modals/HelpComponent.vue'
import UserStore from './Pages/Utils/UserStore'

interface Component {
  id: string
  component: Object
}

export default {
  components:{
    'help': HelpComponent
  },
  async mounted() {
    this.user = await UserStore.user();
    this.isClient = UserStore.isClient(this.user);
  },
  setup() {
    const showMenu = ref(false);

    const user = ref({});
    const isClient = ref(false);

    const components = ref([
      {id: "main", component: MainPageComponent},
      {id: "price", component: PricePageComponent},
    ]);

    watch(isClient, (newValue, oldValue) => {
      if(newValue == oldValue) return;

      if(newValue){
        const cabinetPage: HTMLElement = null;
        components.value.push({page: cabinetPage, id: "cabinet", component: CabinetPageComponent})
      }else{
        components.value.pop();
      }
    });
  
    const currentIndex = ref(0)

    const handleWheel = (event: WheelEvent) => {
        event.preventDefault();
      if (event.deltaY > 0) {
        // Scroll down
        currentIndex.value = Math.min(currentIndex.value + 1, components.value.length - 1)
      } else {
        // Scroll up
        currentIndex.value = Math.max(currentIndex.value - 1, 0)
      }
      scrollToComponent()
    }

    const handleScrollToComponent = (pageId: string) => {
      components.value.forEach((c, i) => {
        if(pageId == c.id) currentIndex.value = i;
      });
      scrollToComponent();
    }

    const shortCloseClickHandler = () => {
      showMenu.value = !showMenu.value
    }

    const scrollToComponent = () => { 
        window.document.getElementById(components.value[currentIndex.value].id).scrollIntoView({ behavior: "smooth", block: "start", inline: "nearest" });
    }

    onMounted(() => {       
        document.addEventListener('wheel', handleWheel, {passive: false});
    });
    return {
      components,
      currentIndex,
      handleWheel,
      handleScrollToComponent,
      shortCloseClickHandler,
      showMenu,
      user,
      isClient
    }
  },

  methods: {
    async logOutHandler(){
      this.user = {};
      this.isClient = false;
    }
  }
}
</script>

<style>

body{
  font:"EB Garamond", serif !important  ;
  overflow: hidden;
}

.full-screen-slider {
  overflow: none;
  scroll-behavior: smooth;
}

.full-screen-component {
  position: relative;
  z-index: 1;
  height: 100vh;
  width: 100vw;
}

.full-screen-component {
  display: flex;
  justify-content: center;
  align-items: center;
  transition: transform 0.5s;
}
</style>