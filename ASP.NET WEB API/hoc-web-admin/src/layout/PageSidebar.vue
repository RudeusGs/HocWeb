<template>
  <aside id="sidebar" class="js-sidebar" :class="showItems ? 'collapsed' : ''">
    <div class="h-100">
      <div class="sidebar-logo">
        <a href="/">
          <img src="@/assets/hw.png" class="avatar" alt=""
        />Học web admin</a>
      </div>
      <ul class="sidebar-nav boder-top">
        <li class="sidebar-item">
          <a href="/" class="sidebar-link">
            <i class="fas fa-tachometer-alt me-2"></i>
            Tổng quát
          </a>
        </li>
        <li class="sidebar-item">
          <a href="/order" class="sidebar-link">
            <i class="fas fa-shopping-cart me-2"></i>
            Đơn hàng
          </a>
        </li>
        <li class="sidebar-item">
          <a href="/category" class="sidebar-link">
            <i class="far fa-list-alt me-2"></i>
            Danh mục
          </a>
        </li>
        <li class="sidebar-item">
          <a href="/product" class="sidebar-link">
            <i class="fas fa-truck me-2"></i>
            Sản phẩm
          </a>
        </li>
        <li class="sidebar-item">
          <a href="/user" class="sidebar-link">
            <i class="fas fa-users me-2"></i>
            Tài khoản
          </a>
        </li>
      </ul>
    </div>
  </aside>
</template>

<script setup lang="ts">
import { defineProps, computed } from "vue";

interface Props {
  toggled?: boolean;
}

const props = defineProps<Props>();

const showItems = computed(() => props.toggled);
</script>
