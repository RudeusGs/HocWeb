<template>
  <div class="wrapper">
    <PageSidebar :toggled="menuToggle"/>
    <div class="main">
      <PageNavbar :menuToggleClick="menuToggleClick"/>
      <main class="content px-3 py-2">
        <div class="container-fluid">
          <RouterView />
        </div>
      </main>
      <ThemeToggle />
    </div>
  </div>
</template>

<script setup lang="ts">
import PageNavbar from "./PageNavbar.vue";
import PageSidebar from "./PageSidebar.vue";
import ThemeToggle from "@/components/ThemeToggle.vue";
import { ref } from "vue";


const menuToggle = ref(false);
const headerTitle = ref<string | undefined>();

const menuToggleClick = () => {
  menuToggle.value = !menuToggle.value;
};

</script>
