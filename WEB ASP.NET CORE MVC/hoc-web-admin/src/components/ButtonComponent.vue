<template>
     <button class="btn" :disabled="disabled" 
     :class="[className, theme=='light'?'btn-dark':'btn-light']"
      @click="onClick">{{text}}
      </button>
</template>

<script setup lang="ts">
import { defineProps } from "vue";
import { storeToRefs } from 'pinia';
import { useThemeStore } from "@/stores/themeStore";

const themeStore = useThemeStore();
const { theme } = storeToRefs(themeStore);

interface Props {
  disabled?:boolean
  className?:string
  text?:string
  onClick?: () => void; 
}

withDefaults(defineProps<Props>(), {
    disabled: false,
    className: '',
    text:'Button',
    onClick: Function,
});
</script>