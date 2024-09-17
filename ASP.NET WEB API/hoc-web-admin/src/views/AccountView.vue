<template>
  <div class="mb-3">
    <h4>Quản lý tài khoản</h4>
  </div>
  <GroupItem title="Thông tin tài khoản">
    <div class="row">
      <div class="row col-sm-4">
        <label for="userId" class="col-sm-4 col-form-label">Mã tài khoản</label>
        <div class="col-sm-8">
          <input
            class="form-control"
            v-model="selected.id"
            disabled
            id="userId"
          />
        </div>
      </div>
      <div class="row col-sm-6">
        <label for="userName" class="col-sm-3 col-form-label">Tên tài khoản</label>
        <div class="col-sm-9">
          <input
            class="form-control"
            v-model="selected.userName"
            disabled
            id="userName"
          />
        </div>
      </div>
      <ButtonComponent
        text="Thêm"
        :onClick="onAdd"
        class-name="col-sm-1"
      />
      <ButtonComponent :disabled="updateStatus" text="Sửa" 
      :on-click="onUpdate" class-name="ms-2 col-sm-1" />
    </div>
  </GroupItem>
  <div>
    <h4>Danh sách tài khoản</h4>
    <table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Tên tài khoản</th>
          <th scope="col">Email</th>
          <th scope="col">Họ và tên</th>
          <th scope="col">Quyền</th>
          <th scope="col">Xóa</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(account, index) in accounts" :key="account.id" @click="rowClick(account)">
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ account.userName }}</td>
          <td>{{ account.email }}</td>
          <td>{{ account.fullName }}</td>
          <td>{{ account.roles.join(', ') }}</td>
          <td class="text-danger">
            <a @click.prevent="confirmDelete(account.id)">Xóa</a>
          </td>
        </tr>
      </tbody>
    </table>
  </div>

  <DialogComponent
    :modelValue="isAddOpen"
    title="Thêm tài khoản"
    @update:modelValue="isAddOpen = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <AccountDetailPage :on-close="onClose" :mode="'create'" />
    </template>
  </DialogComponent>

  <DialogComponent
    :modelValue="isUpdateOpen"
    title="Cập nhật tài khoản"
    @update:modelValue="isUpdateOpen = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <AccountDetailPage :on-close="onClose" :mode="'update'" v-model="selected"/>
    </template>
  </DialogComponent>

  <DialogComponent
    :modelValue="isAssignRolesOpen"
    title="Phân quyền tài khoản"
    @update:modelValue="isAssignRolesOpen = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <AssignRolesDialog :on-close="onClose" v-model="selected" />
    </template>
  </DialogComponent>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import GroupItem from "@/components/GroupItem.vue";
import ButtonComponent from "@/components/ButtonComponent.vue";
import DialogComponent from "@/components/DialogComponent.vue";
import AccountDetailPage from "@/views/pages/AccountDetailPage.vue";
import accountApi from "@/api/account.api";

const accounts = ref<any[]>([]);
const selected = ref({
  id: undefined,
  userName: "",
  email: "",
  fullName: "",
  roles: [],
});
const isAddOpen = ref(false);
const isUpdateOpen = ref(false);
const isAssignRolesOpen = ref(false);

const onAdd = () => {
  isAddOpen.value = true;
};

const onUpdate = () => {
  isUpdateOpen.value = true;
};

const onAssignRoles = (account: any) => {
  selected.value = { ...account };
  isAssignRolesOpen.value = true;
};

const onDialogClose = async () => {
  await loadAccounts();
};

const updateStatus = computed(() => !selected.value.id);

const rowClick = (account: any) => {
  selected.value = { ...account };
};

const loadAccounts = async () => {
  try {
    const response = await accountApi.getAllAccounts();
    if (response && response.data.isSuccess) {
      accounts.value = response.data.data;
    } else {
      accounts.value = [];
      alert(response.data.message);
    }
  } catch (error) {
    alert('Lỗi');
  }
};

const confirmDelete = async (accountId: number) => {
  try {
    const confirmDelete = confirm("Bạn có chắc chắn muốn xóa tài khoản này?");
    if (!confirmDelete) return;

    const response = await accountApi.deleteAccount(accountId);
    if (response && response.data.isSuccess) {
      alert("Tài khoản đã được xóa thành công");
      await loadAccounts();
    } else {
      alert(response.data.message);
    }
  } catch (error) {
    alert('Lỗi');
  }
};

onMounted(loadAccounts);
</script>
