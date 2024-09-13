<template>
  <div class="container-xxl py-5">
    <div class="container">
      <div class="text-center wow fadeInUp" data-wow-delay="0.1s">
        <h6 class="section-title text-center fw-bold px-3">Đăng Nhập</h6>
      </div>
      <div class="row g-4 justify-content-center">
        <div class="container">
          <div class="row justify-content-center text-center">
            <div class="col-lg-4 pt-lg-3 mt-lg-3 text-center">
              <div class="position-relative w-75 mx-auto animated slideInDown">
                <input
                  class="form-control border-2 rounded-pill w-100 py-3 ps-4 pe-5"
                  type="text"
                  placeholder="Tài khoản"
                  v-model="userName"
                />
              </div>
              <div
                class="position-relative w-75 mx-auto mt-3 animated slideInDown"
              >
                <input
                  class="form-control border-2 rounded-pill w-100 py-3 ps-4 pe-5"
                  type="password"
                  placeholder="Mật khẩu"
                  v-model="password"
                />
              </div>
              <button
                type="button"
                class="btn rounded-pill py-2 px-4 text-light mt-3 bg-dark me-2"
                @click="handleLogin"
              >
                Đăng nhập
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import authApi from "@/api/authenticate.api";
import Cookies from "js-cookie";
import { useRouter } from "vue-router";
import { userStore } from "../stores/auth";

const user = userStore();
const userName = ref("");
const password = ref("");
const router = useRouter();

const handleLogin = async () => {
  if (userName.value && password.value) {
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
  } else {
    alert("Tài khoản và mật khẩu không được để trống.");
  }
};
</script>
