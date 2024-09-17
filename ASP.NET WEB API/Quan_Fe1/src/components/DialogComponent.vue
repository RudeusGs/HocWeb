<template>
  <teleport to="body">
    <div v-if="isOpen" class="modal fade show d-block" tabindex="-1" aria-modal="true" role="dialog">
      <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title text-center w-100">{{ title ?? 'Dialog' }}</h5>
            <button type="button" class="btn-close" @click="close" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <slot></slot>
          </div>
          <div class="modal-footer d-flex justify-content-end">
            <slot name="footer">
              <button type="button" class="btn btn-dark" @click="close">Đóng</button>
            </slot>
          </div>
        </div>
      </div>
    </div>
  </teleport>
</template>
  
  <script setup lang="ts">
  import { ref, defineProps, defineEmits, watch } from 'vue';
  
  const props = defineProps<{
    modelValue: boolean;
    title?: string
  }>();
  
  const emit = defineEmits<{
    (e: 'update:modelValue', value: boolean): void;
  }>();
  
  const isOpen = ref(props.modelValue);
  
  const close = () => {
    isOpen.value = false;
    emit('update:modelValue', false);
  };
  
  watch(() => props.modelValue, (newVal) => {
    isOpen.value = newVal;
  });
  </script>