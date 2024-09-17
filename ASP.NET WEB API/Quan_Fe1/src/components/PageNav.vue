<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
  <div class="container-fluid" style="margin-left: 100px">
    <a class="navbar-brand" href="/">ShopLOL</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a class="nav-link active" aria-current="page" href="/">Trang chủ</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="/about">Giới thiệu</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="#">Liên hệ</a>
        </li>
        <li class="nav-item dropdown">
          <a
            class="nav-link dropdown-toggle"
            id="navbarDropdown"
            role="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Danh Mục
          </a>
          <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li v-for="item in categories" :key="item.id">
              <a
                class="dropdown-item"
                :href="'/product?categoryId=' + item.id"
              >
                {{ item.nameVN }}
              </a>
            </li>
          </ul>
        </li>
      </ul>
      <form class="d-flex" style="margin-right: 30px" v-if="user">
        <a class="btn btn-outline-secondary" href="/cartview">Giỏ hàng</a>
      </form>
      <form class="d-flex" style="margin-right: 100px">
        <ul class="navbar-nav mb-0">
          <li v-if="!user" class="nav-item">
            <a class="nav-link fw-bold" href="/login">Đăng Nhập</a>
          </li>
          <li v-else class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              id="navbarDropdown"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
             <strong>{{ user?.fullName }}</strong>
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
              <li><a class="dropdown-item" href="/infor_use">Thông tin</a></li>
              <li><a class="dropdown-item" href="/order">Lịch sử</a></li>
              <li><hr class="dropdown-divider"></li>
              <li>
                <a class="dropdown-item" @click="logout" href="/">Đăng xuất</a>
              </li>
            </ul>
          </li>
        </ul>
      </form>
    </div>
  </div>
</nav>
</template>
<script setup lang="ts">
import navApi from "@/api/nav.api";
import {  onMounted } from "@vue/runtime-core";
import { ref } from "vue";
import { userStore } from "../stores/auth";
import { useCartStore } from "../stores/cart";
import { storeToRefs } from "pinia";
import Cookies from "js-cookie";

const authStore = userStore();
const { user } = storeToRefs(authStore);

const useCart = useCartStore();
const { items } = storeToRefs(useCart);

const categories = ref<any>([]);

const fetchData = async () => {
  const response = await navApi.getAllCategory();
  if (response && response.data.result.isSuccess) {
    categories.value = response.data.result.data;
  } else {
    categories.value = [];
    alert(response.data.result.message);
  }
};

const logout = async () => {
  Cookies.remove("token");
  authStore.logout();
};

onMounted(() => {
  useCart.loadCart();
  fetchData();
});
</script>