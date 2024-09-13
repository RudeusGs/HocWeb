<template>
  <div class="page-container">
    <section class="container">
      <div class="row justify-content-center mb-5">
        <div class="col-md-6 text-center">
         
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-md-6 col-lg-4">
          <div class="login-wrap p-4 p-md-5">
            <h3 class="mb-4 text-center">Đăng nhập</h3>
            <form class="signin-form" @submit.prevent="handleLogin">
              <div class="form-group mb-3">
                <input v-model="userName" type="text" class="form-control" placeholder="Tài khoản" required>
              </div>
              <div class="form-group mb-3 position-relative">
                <input v-model="password" type="password" class="form-control" placeholder="Mật khẩu" required>
                <span class="fa fa-fw fa-eye field-icon toggle-password"></span>
              </div>
              <div class="form-group">
                <button type="submit" class="form-control btn btn-primary submit px-3">Đăng nhập</button>
              </div>
              <div class="form-group d-flex justify-content-between">
                <div class="w-100 text-end">
                  <router-link to="/register" style="color: #000">Chưa có tài khoản?</router-link>
                </div>
              </div>
            </form>
            <p class="w-100 text-center mt-4">Đăng nhập với</p>
            <div class="social d-flex justify-content-center">
              <a href="#" class="px-2 py-2 mr-1 rounded"><span class="fab fa-facebook-f mr-2"></span> Facebook</a>
              <a href="#" class="px-2 py-2 ml-1 rounded"><span class="fab fa-twitter mr-2"></span> Twitter</a>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import authApi from "@/api/authenticate.api";
import { ref } from "vue";
import Cookies from "js-cookie";
import { useRouter } from "vue-router";
import { userStore } from "../stores/auth";

const user = userStore();
const userName = ref("");
const password = ref("");
const router = useRouter();

const handleLogin = async () => {
  if (userName.value && password.value) {
    try {
      const loginModel = {
        userName: userName.value,
        password: password.value,
      };

      const response = await authApi.login(loginModel);

      if (response && response.result?.isSuccess) {
        user.login({
          userId: response.result.data.userId,
          username: response.result.data.username,
          email: response.result.data.email,
          fullName: response.result.data.fullName,
        });
        Cookies.set("token", response.result.data.token);
        router.push("/");
      } else {
        alert("Sai tài khoản hoặc mật khẩu.");
      }
    } catch (error) {
      console.error("Error during login:", error);
      alert("Có lỗi xảy ra. Vui lòng thử lại sau.");
    }
  } else {
    alert("Tài khoản và mật khẩu không được để trống.");
  }
};
</script>

<style scoped>
.login-wrap {
  background: #f7f7f7;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}
</style>
