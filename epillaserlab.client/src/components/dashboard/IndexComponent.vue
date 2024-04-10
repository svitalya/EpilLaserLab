<template>
    <headerComponent></headerComponent>
    <div class="container-fluid">
        <div class="row">
            <sidebar></sidebar>
            <main class="col-md-9 ml-sm-auto col-lg-10 px-md-4 py-4">
                <router-view></router-view>
            </main>
        </div>
    </div>

</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import HeaderComponent  from './HeaderComponent.vue'
    import sidebar from './SideBarComponent.vue'

    export default defineComponent({
        components: {
            HeaderComponent,
            sidebar
        }
    });
</script>

<style>
</style>