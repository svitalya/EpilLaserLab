<template>
    <headerComponent></headerComponent>
    <div class="container-fluid">
        <div class="row">
            <sidebar></sidebar>
            <main class="col-10 ml-sm-auto px-md-4 py-4">
                <div class="row col-12 d-flex justify-content-center">
                    <router-view class="row"/>
                </div>
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