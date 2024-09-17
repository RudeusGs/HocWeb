<template>
  <header class="py-5 bg-light">
    <div class="container px-4 px-lg-5 my-5">
      <div class="d-flex justify-content-center align-items-center" style="height: 300px;">
        <img src="../assets/a.png" class="img-fluid display-4 fw-bolder" alt="Logo">
      </div>
    </div>
  </header>

  <section class="featured-accounts py-5">
    <div class="container">
      <h2 class="fw-bolder mb-4 text-center">Các tài khoản hot</h2>
      <div class="row g-4">
        <div class="col-md-4" v-for="(item, index) in products" :key="index">
          <div class="card h-100 shadow-sm">
            <a :href="'/product-detail?id=' + item.id">
              <img class="card-img-top" :src="item.image" alt="Product image" />
            </a>
            <div class="card-body d-flex flex-column">
              <h5 class="card-title fw-bolder">{{ item.name }}</h5>
              <p class="card-text text-muted">{{ new Intl.NumberFormat("vi-vn").format(item.unitPrice) }}đ</p>
              <div class="mt-auto d-flex justify-content-between">
                <a class="btn btn-outline-secondary" :href="'/product-detail?id=' + item.id">Xem</a>
                <button class="btn btn-outline-secondary" @click="addToCart(item)">Thêm vào giỏ hàng</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>



<script setup lang="ts">
import homeApi from "@/api/home.api";
import { ref } from "vue";
import { useCartStore } from "@/stores/cart";
import { storeToRefs } from "pinia";
import { userStore } from "../stores/auth";

const authStore = userStore();
const { user } = storeToRefs(authStore);

const products = ref<any>([]);
const categories = ref<any>([]);

const addToCart = (item: any) => {
  if (!user.value) {
    alert("Hãy đăng nhập để thực hiện chức năng này");
    return;
  }

  const product = {
    id: item.id,
    name: item.name,
    unitPrice: item.unitPrice,
    quantity: 1,
  };
  useCartStore().addToCart(product);
};

const fetchData = async () => {
  const response = await homeApi.getAllProduct();
  if (response && response.data.result.isSuccess) {
    products.value = response.data.result.data;
  } else {
    products.value = [];
    alert(response?.data.result.message ?? "Lỗi");
  }

  const response2 = await homeApi.getAllCategory();
  if (response2 && response2.data.result.isSuccess) {
    categories.value = response2.data.result.data;
  } else {
    categories.value = [];
    alert(response2?.data.result.message ?? "Lỗi");
  }
};

fetchData();
</script>

<style scoped>
  
</style>
