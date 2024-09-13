<template>
  <section class="py-5">
    <div class="container">
      <div class="row">
        <!-- Order Summary Section -->
        <div class="col-md-7">
          <div v-if="items.filter((x) => x.checked).length">
            <div class="order-summary p-3 mb-4">
              <h4 class="fw-bold mb-3">Đơn hàng</h4>
              <div class="table-responsive">
                <table class="table">
                  <thead>
                    <tr class="text-center">
                      <th>#</th>
                      <th>Sản phẩm</th>
                      <th>Đơn giá</th>
                      <th>Số lượng</th>
                      <th>Thành tiền</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(item, index) in items.filter((x) => x.checked)" :key="index">
                      <td class="text-center">{{ index + 1 }}</td>
                      <td>{{ item.name }}</td>
                      <td class="text-end">{{ new Intl.NumberFormat("vi-vn").format(item.unitPrice) }}đ</td>
                      <td class="text-center">{{ item.quantity }}</td>
                      <td class="text-end">{{ new Intl.NumberFormat("vi-vn").format(item.quantity * item.unitPrice) }}đ</td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <div class="text-end mt-3">
                <h5 class="fw-bold">Tổng tiền: {{ new Intl.NumberFormat("vi-vn").format(useCart.chosedTotal()) }}đ</h5>
              </div>
            </div>
          </div>
          <div v-else>
            <p class="text-center">Hiện không có sản phẩm nào được chọn</p>
          </div>
        </div>

        <!-- Order Information Section -->
        <div class="col-md-5">
          <div class="order-info p-3 mb-4">
            <h4 class="fw-bold mb-3 text-center">Thông tin người nhận</h4>
            <div class="mb-3">
              <label class="form-label" for="name">Tên bạn</label>
              <input type="text" class="form-control" :value="user?.username" placeholder="Tên" disabled />
            </div>
            <div class="mb-3">
              <label class="form-label" for="email">Email</label>
              <input type="email" class="form-control" :value="user?.email" placeholder="Email" disabled />
            </div>
            <div class="mb-3">
              <label class="form-label" for="address">Địa chỉ</label>
              <input type="text" class="form-control" v-model="address" placeholder="Địa chỉ" />
            </div>
            <div class="mb-3">
              <label class="form-label" for="description">Ghi chú</label>
              <textarea v-model="description" class="form-control" rows="4" placeholder="Ghi chú"></textarea>
            </div>
            <div class="text-center">
              <button class="btn btn-primary fw-bold" @click="onCheckout">Đặt hàng</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>




<script setup lang="ts">
import {ref} from "vue"
import { storeToRefs } from "pinia";
import { useCartStore } from "../stores/cart";
import { userStore } from "../stores/auth";
import checkoutApi from "@/api/checkout.api";

const useCart = useCartStore();
const { items } = storeToRefs(useCart);
const authStore = userStore();
const { user } = storeToRefs(authStore);
const address = ref()
const description = ref()

const onCheckout = async ()=>{
  const selectItem = items.value.filter(x=>x.checked)
  if(!selectItem){
    alert("Không có sản phẩm nào được chọn")
    return
  }

  const payload = {
    customerId: user.value?.userId,
    address: address.value,
    description: description.value,
    amount: useCart.chosedTotal(),
    orderDetails: selectItem.map(x=> {
        return {
            productId: x.id,
            unitPrice: x.unitPrice,
            quantity: x.quantity,
          }})
  }

  
  const response = await checkoutApi.createNewOrder(payload);
  if (response && response.data.result.isSuccess) {
    alert('Đã đặt hàng thành công!');
    useCart.clearSelectedCart();
  } else {
    alert(response?.data.result.message??'Lỗi');
  }
}

</script>
