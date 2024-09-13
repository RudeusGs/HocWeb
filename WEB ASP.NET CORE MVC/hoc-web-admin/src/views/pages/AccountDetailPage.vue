<template>
  <div>
    <form @submit.prevent="onSubmit">
      <div class="mb-3">
        <label for="username" class="form-label">Tên tài khoản</label>
        <input
          type="text"
          class="form-control"
          v-model="user.username"
          id="username"
        />
      </div>
      <div class="mb-3">
        <label for="email" class="form-label">Email</label>
        <input
          type="email"
          class="form-control"
          v-model="user.email"
          id="email"
          required
        />
      </div>
      <div class="mb-3">
        <label for="fullName" class="form-label">Họ và tên</label>
        <input
          type="text"
          class="form-control"
          v-model="user.fullName"
          id="fullName"
          required
        />
      </div>
      <div class="mb-3" v-if="props.mode === 'create'">
        <label for="password" class="form-label">Mật khẩu</label>
        <input
          type="password"
          class="form-control"
          v-model="password"
          id="password"
          required
        />
      </div>
      <div class="mb-3">
        <label for="roles" class="form-label">Quyền</label>
        <select v-model="selectedRoles" class="form-control" multiple>
          <option v-for="role in allRoles" :key="role" :value="role">{{ role }}</option>
        </select>
      </div>
      <button type="submit" class="btn btn-primary">Lưu</button>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, defineProps } from 'vue';
import accountApi from "@/api/account.api";
import { type UserInfoModel } from '@/models/user-model';

const props = defineProps({
  onClose: Function,
  mode: String,
  modelValue: Object as () => UserInfoModel | undefined
});

const allRoles = ref<string[]>(['Admin', 'User']);
const selectedRoles = ref<string[]>([]);

const user = ref<UserInfoModel>({
  username: '',
  userId: 0,
  email: '',
  fullName: '',
  roles: []
});

const password = ref<string>('');

watch(
  () => props.modelValue,
  (newValue) => {
    if (newValue) {
      user.value = { ...newValue };
      selectedRoles.value = newValue.roles;
    }
  },
  { immediate: true }
);

const onSubmit = async () => {
  try {
    if (!user.value) {
      alert('Không có dữ liệu tài khoản để lưu.');
      return;
    }

    const formData = new FormData();
    formData.append('id', user.value.userId.toString());
    formData.append('userName', user.value.username);
    formData.append('email', user.value.email);
    formData.append('fullName', user.value.fullName || '');
    if (props.mode === 'create') {
      formData.append('password', password.value);
    }
    formData.append('roles', JSON.stringify(selectedRoles.value));

    let response;
    if (props.mode === 'create') {
      response = await accountApi.addUser(formData);
    } else {
      response = await accountApi.updateAccount(formData);
    }

    if (response && response.data.isSuccess) {
      alert('Thành công');
      if (props.onClose) props.onClose();
    }
  } catch (error) {
    alert('lỗi');
  }
};
</script>
