<template>
<nav class="navbar navbar-light bg-light p-3">
  <div class="col-10 col-md-10 col-lg-10 d-flex align-items-center justify-content-md-end mt-3 mt-md-0">
      <div class="dropdown">
          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-expanded="false">
              Доброй пожаловать, John Doe
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
              <li><a class="dropdown-item" href="#">Выход </a></li>
          </ul>
      </div>
  </div>
</nav>
</template>
<script>
import { defineComponent } from 'vue';
export default defineComponent({})
</script>
