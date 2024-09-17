<template>
  <section class="py-5">
    <div class="container px-4 px-lg-5">
      <h2 class="fw-bolder mb-4 text-center">{{ categoryName ?? "Tất cả sản phẩm" }}</h2>
      <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4">
        <div v-for="(item, index) in products" :key="index" class="col mb-4">
          <div class="card h-100 shadow-sm">
            <div class="position-relative">
              <a :href="'/product-detail?id=' + item.id">
                <img class="card-img-top" :src="item.image" alt="Product Image" />
              </a>
            </div>
            <div class="card-body text-center">
              <h5 class="card-title fw-bolder">{{ item.name }}</h5>
              <div class="d-flex justify-content-center mb-2">
                <div class="bi-star-fill text-warning"></div>
                <div class="bi-star-fill text-warning"></div>
                <div class="bi-star-fill text-warning"></div>
              </div>
              <div class="mb-3">{{ new Intl.NumberFormat("vi-vn").format(item.unitPrice) }}đ</div>
              <div class="d-grid gap-2">
                <a :href="'/product-detail?id=' + item.id" class="btn btn-outline-dark">Xem chi tiết</a>
                <button class="btn btn-outline-dark" @click="addToCart(item)">Thêm vào giỏ hàng</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRoute } from "vue-router";
import productApi from "@/api/product.api";
import { useCartStore } from "@/stores/cart";
import { storeToRefs } from "pinia";
import { userStore } from "../stores/auth";

const authStore = userStore();
const { user } = storeToRefs(authStore);

const route = useRoute();
const products = ref<any>([]);
const categoryName = ref<any>();

const fetchData = async () => {
  if (route.query.categoryId) {
    const cateRes = await productApi.getCategoryById(
      route.query.categoryId?.toString()
    );
    if (cateRes && cateRes.data.result.isSuccess) {
      categoryName.value = cateRes.data.result.data?.nameVN;
    }
  }

  const response = await productApi.getAllProduct();
  if (response && response.data.result.isSuccess) {
    if (route.query.categoryId) {
      products.value = response.data.result.data?.filter(
        (x: any) => x.categoryId == route.query.categoryId
      );
    } else {
      products.value = response.data.result.data;
    }
  } else {
    products.value = [];
    alert(response.data.result.message);
  }
};

const addToCart = (item: any) => {
  if(!user.value){
    alert('Hãy đăng nhập để thực hiện chức năng này')
    return
  }
  
  const product =  {
    id: item.id,
    name: item.name,
    unitPrice: item.unitPrice,
    quantity : 1
  }
  useCartStore().addToCart(product);
};

  fetchData();
</script>
