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
      v-for="componentData in components"
      :is="componentData.component"
      :id="componentData.id"
      :ref="async (el) => componentData.page = el.$el"
      :show-menu="showMenu"
      :key="JSON.stringify(componentData)"
      class="full-screen-component"></component>
  </div>
</template>

<script lang="ts">
import { ref, onMounted, Ref, reactive} from 'vue'
import MainPageComponent  from './Pages/MainPageComponent.vue'
import PricePageComponent  from './Pages/PricePageComponent.vue'
import CabinetPageComponent  from './Pages/CabinetPageComponent.vue'
import HelpComponent from './Pages/Utils/Modals/HelpComponent.vue'
import UserStore from './Pages/Utils/UserStore'

interface Component {
  page: Ref<HTMLElement | null>
  id: string
  component: Object
}

export default {
  components:{
    'help': HelpComponent
  },
  async mounted() {
    let isClient = UserStore.isClient(await UserStore.user());

    if(isClient){
      const cabinetPage = ref<HTMLElement | null>(null);
      this.components.push({page: cabinetPage, id: "cabinet", component: CabinetPageComponent})
    }
  },
  setup() {
    const showMenu = ref(false);
    const mainPage = ref<HTMLElement | null>(null);
    const pricePage = ref<HTMLElement | null>(null);
  
    const components = reactive<Component[]>( [
      {page: mainPage, id: "main", component: MainPageComponent},
      {page: pricePage, id: "price", component: PricePageComponent},
    ]);

    const currentIndex = ref(0)

    const handleWheel = (event: WheelEvent) => {
        event.preventDefault();
      if (event.deltaY > 0) {
        // Scroll down
        currentIndex.value = Math.min(currentIndex.value + 1, components.length - 1)
      } else {
        // Scroll up
        currentIndex.value = Math.max(currentIndex.value - 1, 0)
      }
      scrollToComponent()
    }

    const handleScrollToComponent = (pageId: string) => {
      components.forEach((c, i) => {
        if(pageId == c.id) currentIndex.value = i;
      });
      scrollToComponent();
    }

    const shortCloseClickHandler = () => {
      showMenu.value = !showMenu.value
    }

    const scrollToComponent = () => { 
        components[currentIndex.value].page.scrollIntoView({ behavior: "smooth", block: "start", inline: "nearest" });
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
      showMenu
    }
  }
}
</script>

<style>

body{
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