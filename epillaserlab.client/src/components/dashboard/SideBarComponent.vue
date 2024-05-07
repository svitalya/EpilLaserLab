<template>
<nav id="sidebar" class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
  <div class="position-sticky pt-md-5">
      <ul class="nav flex-column">
          <li :class="[(item.dropdown && item.dropdown.length > 0 ? 'nav-item' : ''), 'dropdown']" v-for="item in navItems">
              <router-link
                :tag="'a'"
                :class="['nav-link', (item.dropdown && item.dropdown.length > 0 ? 'dropdown-toggle' : '')]"
                aria-current="page"
                :data-bs-toggle="[(item.dropdown && item.dropdown.length > 0 ? 'dropdown' : '')]"
                aria-expanded="true"
                :to="{name: item.link, params: item.params}">
                  <svg xmlns="http://www.w3.org/2000/svg" 
                    width="24" height="24" viewBox="0 0 24 24" 
                    fill="none" stroke="currentColor" stroke-width="2" 
                    stroke-linecap="round" stroke-linejoin="round" 
                    v-html="item.svg">
                </svg>
                  <span class="ml-2 ms-2">{{ item.text }}</span>
              </router-link>

              <ul class="dropdown-menu" v-if="item.dropdown && item.dropdown.length > 0">
                    <li v-for="dropdownItem in item.dropdown">
                        <router-link
                            :to = "{name: item.link, params: dropdownItem.params}"
                            class="dropdown-item">
                            {{ dropdownItem.text}}
                        </router-link>
                    </li>
                </ul>
          </li>
      </ul>
  </div>
</nav>
</template>
<script>
import {defineComponent, ref} from 'vue';

export default defineComponent({
    setup(){
        const navItems = ref({});
        fetch("https://localhost:7243/api/dashboard/sidebar", {
            method: "GET",
            headers: {'Content-Type': "application/json"},
            credentials: "include",  
        }).then(async r => {
            const responseJson = await r.json();
            navItems.value = responseJson.navItems;
        })
        return {navItems}
    }
})

</script>
