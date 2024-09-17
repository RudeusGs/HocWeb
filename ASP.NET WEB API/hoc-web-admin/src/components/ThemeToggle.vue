<template>
  <a class="theme-toggle" @click="themeStore.toggleTheme">
    <i class="fas fa-moon"></i>
    <i class="fas fa-sun"></i>
  </a>
</template>
<script setup lang="ts">
import { useThemeStore } from "@/stores/themeStore";

const themeStore = useThemeStore();
themeStore.checkInitialTheme();
</script>
