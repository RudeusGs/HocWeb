<template>
  <section class="pt-1">
    <div class="container px-4 px-lg-5 mt-1" v-if="orders.length">
      <h2 class="fw-bolder mb-4 text-center">Các đơn hàng của bạn</h2>
      <div class="table-responsive">
        <table class="table table-bordered table-hover">
          <thead class="table-dark">
            <tr class="text-center">
              <th scope="col">STT</th>
              <th scope="col">Mã đơn hàng</th>
              <th scope="col">Ngày mua</th>
              <th scope="col">Số tiền</th>
              <th scope="col">Địa chỉ nhận</th>
              <th scope="col">Ghi chú</th>
              <th scope="col">Tùy chọn</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in orders" :key="index">
              <th scope="row" class="text-center">{{ index + 1 }}</th>
              <td>{{ item.id }}</td>
              <td>{{ item.createdDate ? moment(item.createdDate).format('DD/MM/YYYY') : '' }}</td>
              <td class="text-end">
                {{ item.amount ? new Intl.NumberFormat("vi-vn").format(item.amount) : '' }}đ
              </td>
              <td>{{ item.address }}</td>
              <td>{{ item.description }}</td>
              <td class="text-center">
                <button class="btn btn-outline-primary btn-sm" @click="onDetailClick(item)">Xem</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="container px-4 px-lg-5 mt-1" v-else>
      <h2 class="fw-bolder mb-4 text-center">Chưa có sản phẩm nào</h2>
    </div>
  </section>
  <DialogComponent :modelValue="isDialogOpen" :title="title" @update:modelValue="isDialogOpen = $event">
    <OrderDetailView :orderId="selectedOrder?.id" />
  </DialogComponent>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { storeToRefs } from "pinia";
import { userStore } from "../stores/auth";
import orderApi from "@/api/order.api";
import moment from 'moment';
import DialogComponent from '@/components/DialogComponent.vue'

const authStore = userStore();
const { user } = storeToRefs(authStore);

const orders = ref<any>([]);
const isDialogOpen = ref(false);
const title = ref('Chi tiết đơn hàng');
const selectedOrder = ref<any>();

const fetchData = async () => {
  const response = await orderApi.getAllOrder();
  if (response && response.data.result.isSuccess) {
    orders.value = response.data.result.data
  } else {
    orders.value = [];
    alert(response.data.result.message);
  }
};

const onDetailClick = (item: any) => {
  title.value = 'Chi tiết đơn hàng ' + item.id
  selectedOrder.value = item;
  isDialogOpen.value = true;
};

fetchData()
</script>