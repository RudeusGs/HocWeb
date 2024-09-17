<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import PageNav from '@/components/PageNav.vue'
import PageFooter from '@/components/PageFooter.vue'
</script>

<template>
  <div class="app-container">
    <PageNav />
    <main class="main-content">
      <RouterView />
    </main>
    <PageFooter />
  </div>
</template>

<style scoped>
html, body {
  height: 100%;
  margin: 0;
}

.app-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.main-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
}
</style>
