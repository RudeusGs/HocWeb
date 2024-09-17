<template>
  <teleport to="body">
    <div
      v-if="model"
      class="modal fade show d-block"
      tabindex="-1"
      aria-modal="true"
      role="dialog"
    >
      <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              {{ title ?? "Dialog" }}
            </h5>
            <button
              type="button"
              class="btn-close"
              @click="close"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <slot :on-close="close" ></slot>
          </div>
          <!-- <div class="modal-footer">
              <slot name="footer">
                <button type="button" class="btn btn-secondary" @click="close">Đóng</button>
              </slot>
            </div> -->
        </div>
      </div>
    </div>
  </teleport>
</template>

<script setup lang="ts">
import { defineProps,defineEmits } from "vue";

interface Props {
  title?: string;
}
const emits = defineEmits(['onDialogClose']);
const props = defineProps<Props>();
const model = defineModel();

const close = () => {
  model.value = false;
  emits('onDialogClose');
};
</script>
