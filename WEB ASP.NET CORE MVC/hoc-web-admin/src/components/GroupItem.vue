<template>
  <div class="card border-0">
    <div class="card-header" v-if="title">
      <h5 class="card-title">{{ title }}</h5>
      <h6 v-if="description" class="card-subtitle text-muted">
        {{ description }}
      </h6>
    </div>
    <div class="card-body">
      <slot></slot>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps } from "vue";

interface Props {
  title?: string;
  description?: string;
}

withDefaults(defineProps<Props>(), {
  title: '',
  description: ''
});
</script>
