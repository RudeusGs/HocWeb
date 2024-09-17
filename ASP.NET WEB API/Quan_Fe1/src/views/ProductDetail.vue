<template>
  <div>
    <header class="py-5">
      <div class="container px-4 px-lg-5 my-5">
        <div>
          <img src="../assets/a.png" class="display-4 fw-bolder" alt="Logo">
        </div>
      </div>
    </header>

    <section class="py-1">
      <div class="container px-4 px-lg-5 my-5">
        <div class="row gx-4 gx-lg-5 align-items-center">
          <div class="col-md-6">
            <img class="img-fluid rounded mb-4 mb-md-0" :src="product?.image" alt="Product Image">
          </div>
          <div class="col-md-6">
            <h1 class="display-5 fw-bolder">{{ product?.name }}</h1>
            <p class="lead">{{ product?.description }}</p>
            <div class="d-flex mb-4">
              <input
                class="form-control text-center me-3"
                type="number"
                v-model="quantity"
                style="max-width: 3rem"
              />
              <button class="btn btn-outline-secondary" @click="addToCart(product)">
                <i class="bi-cart-fill me-1"></i>
                Thêm vào giỏ hàng
              </button>
            </div>
            <div class="fs-5">
              <span>{{ new Intl.NumberFormat("vi-vn").format(product.unitPrice) }}đ</span>
            </div>
          </div>
        </div>
      </div>
    </section>

    <section class="bg-light pt-4 pb-5">
      <div class="container px-2 px-lg-5">
        <h2 class="fw-bolder mb-4">Các sản phẩm cùng loại</h2>
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
          <div v-for="(item, index) in products" :key="index" class="col mb-5">
            <div class="card h-100">
              <router-link :to="'/product-detail?id=' + item.id">
                <img class="card-img-top" :src="item.image" alt="Product Image">
              </router-link>
              <div class="card-body">
                <h5 class="card-title fw-bold">{{ item.name }}</h5>
                <p class="card-text">{{ new Intl.NumberFormat("vi-vn").format(item.unitPrice) }}đ</p>
              </div>
              <div class="card-footer">
                <router-link class="btn btn-outline-secondary me-2" :to="'/product-detail?id=' + item.id">
                  Xem
                </router-link>
                <button class="btn btn-outline-secondary" @click="addToCart(item)">
                  Thêm vào giỏ hàng
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRoute } from "vue-router";
import productDApi from "@/api/product-detail.api";
import { useCartStore } from "@/stores/cart";
import { storeToRefs } from "pinia";
import { userStore } from "../stores/auth";

const authStore = userStore();
const { user } = storeToRefs(authStore);

const route = useRoute();
const product = ref<any>();
const products = ref<any>([]);
const quantity = ref<number>(1);

const fetchData = async () => {
  if (route.query.id) {
    const res = await productDApi.getProductById(route.query.id?.toString());
    if (res && res.data.result.isSuccess) {
      product.value = res.data.result.data;

      const response = await productDApi.getAllProduct();
      if (response && response.data.result.isSuccess) {
        products.value = response.data.result.data?.filter(
          (x: any) => x.categoryId == product.value?.categoryId
        );
      } else {
        products.value = [];
      }
    } else {
      alert(res.data.result.message);
    }
  }
};

const addToCart = (item: any) => {
  if (!user.value) {
    alert('Hãy đăng nhập để thực hiện chức năng này');
    return;
  }

  const product = {
    id: item.id,
    name: item.name,
    unitPrice: item.unitPrice,
    quantity: quantity.value > 0 ? quantity.value : 1,
  };
  useCartStore().addToCart(product);
};

fetchData();
</script>

<style scoped>
</style>
