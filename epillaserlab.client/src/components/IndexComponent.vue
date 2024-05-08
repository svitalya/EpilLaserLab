// FullScreenSlider.vue
<template>
  <div class="full-screen-slider">
    <component 
      v-for="componentData in components"
      :is="componentData.component"
      :id="componentData.id"
      :ref="async (el) => componentData.page.value = el.$el"
      class="full-screen-component"></component>
  </div>
</template>

<script lang="ts">
import { ref, onMounted, Ref} from 'vue'
import MainPageComponent  from './Pages/MainPageComponent.vue'
import PricePageComponent  from './Pages/PricePageComponent.vue'

interface Component {
  page: Ref<HTMLElement | null>
  id: string
  component: Object
}

export default {
  setup() {
    const mainPage = ref<HTMLElement | null>(null);
      const pricePage = ref<HTMLElement | null>(null);

    const components: Component[] = [
      {page: mainPage, id: "main", component: MainPageComponent},
      {page: pricePage, id: "price", component: PricePageComponent}
    ]

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

    const scrollToComponent = () => { 
        components[currentIndex.value].page.value.scrollIntoView({ behavior: "smooth", block: "start", inline: "nearest" });
    }

    onMounted(() => {       
        document.addEventListener('wheel', handleWheel, {passive: false});
    });

    return {
      components,
      currentIndex,
      handleWheel
    }
  }
}
</script>

<style>
body{
    overflow: hidden;
    font-family: "EB Garamond", serif;
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