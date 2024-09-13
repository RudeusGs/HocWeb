<template>
  <section class="shopping-cart-section pt-1" v-if="items">
    <div class="container px-2 px-lg-5 mt-1">
      <h2 class="fw-bolder mb-4 text-center">Giỏ hàng của bạn</h2>
      <div class="row gx-4 gx-lg-5 align-items-center">
        <div class="table-responsive">
          <table class="table table-bordered">
            <thead class="thead-dark">
              <tr class="text-center">
                <th scope="col">STT</th>
                <th scope="col">Chọn</th>
                <th scope="col">Tên sản phẩm</th>
                <th scope="col">Đơn giá</th>
                <th scope="col">Số lượng</th>
                <th scope="col">Thành tiền</th>
                <th scope="col">Tùy chọn</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(item, index) in items" :key="index">
                <th scope="row" class="text-center">{{ index + 1 }}</th>
                <td class="text-center">
                  <input type="checkbox" v-model="item.checked" @change="useCart.updateCheck()" />
                </td>
                <td>{{ item.name }}</td>
                <td class="text-end">{{ formatCurrency(item.unitPrice) }}</td>
                <td class="text-center">{{ item.quantity }}</td>
                <td class="text-end">{{ formatCurrency(item.quantity * item.unitPrice) }}</td>
                <td class="text-center">
                  <button class="btn btn-outline-danger btn-sm" @click="delClick(item)">Xóa</button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="total-section text-end">
          <h5 class="mb-4">
            Tổng tiền toàn giỏ hàng:
            <span class="fw-bold text-success">{{ formatCurrency(useCart.cartTotal()) }}</span>
          </h5>
        </div>
      </div>

      <div class="d-flex justify-content-center my-4">
        <a class="btn btn-outline-secondary fw-bolder" href="/checkoutview">Tiến hành thanh toán</a>
      </div>

      <div class="selected-total-section text-center">
        <h5 class="mb-4">
          Tổng tiền đang chọn:
          <span class="fw-bold text-info">{{ formatCurrency(useCart.chosedTotal()) }}</span>
        </h5>
      </div>
    </div>
  </section>
</template>


<script setup lang="ts">
import { storeToRefs } from "pinia";
import { useCartStore } from "../stores/cart";

const useCart = useCartStore();
const { items } = storeToRefs(useCart);

const delClick = (item: any) => {
  const result = window.confirm("Xác nhận xóa " + item.name);
  if (result) {
    useCart.removeCartItem(item.id);
  }
};

const formatCurrency = (value: number) => {
  return new Intl.NumberFormat("vi-vn", { style: "currency", currency: "VND" }).format(value);
};
</script>

