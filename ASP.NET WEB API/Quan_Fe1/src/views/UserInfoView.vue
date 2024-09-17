<template>
    <section class="user-info-section py-5">
      <div class="container px-4 px-lg-5">
        <div class="row d-flex justify-content-center text-center mb-5">
          <h2 class="fw-bolder col-md-8">Thông tin cá nhân</h2>
        </div>
        
        <div class="row d-flex justify-content-center mb-4">
          <form class="col-md-8 bg-light p-4 rounded shadow">
            <div class="form-group row mb-3">
              <label class="col-sm-3 col-form-label">Tài khoản</label>
              <div class="col-sm-9">
                <input type="text" v-model="userModel.userName" disabled class="form-control bg-light">
              </div>
            </div>
            <div class="form-group row mb-3">
              <label class="col-sm-3 col-form-label">Họ và tên</label>
              <div class="col-sm-9">
                <input type="text" class="form-control" v-model="userModel.fullName" placeholder="Họ và tên">
              </div>
            </div>
            <div class="form-group row mb-3">
              <label class="col-sm-3 col-form-label">Email</label>
              <div class="col-sm-9">
                <input type="email" class="form-control" v-model="userModel.email" placeholder="Email">
              </div>
            </div>
            <div class="d-flex justify-content-end">
              <button type="button" class="btn btn-outline-secondary fw-bold" @click="onUpdaterInfo">Cập nhật</button>
            </div>
          </form>
        </div>
  
        
        <div class="row d-flex justify-content-center">
            <h2 class="d-flex justify-content-center fw-bolder">Thay đổi mật khẩu</h2>
          <form class="col-md-8 bg-light p-4 rounded shadow">
            <div class="form-group row mb-3">
              <label class="col-sm-3 col-form-label">Mật khẩu cũ</label>
              <div class="col-sm-9">
                <input type="password" v-model="passwordModel.oldPass" class="form-control" placeholder="Mật khẩu cũ">
              </div>
            </div>
            <div class="form-group row mb-3">
              <label class="col-sm-3 col-form-label">Mật khẩu mới</label>
              <div class="col-sm-9">
                <input type="password" class="form-control" v-model="passwordModel.newPass" placeholder="Mật khẩu mới">
              </div>
            </div>
            <div class="form-group row mb-3">
              <label class="col-sm-3 col-form-label">Nhập lại mật khẩu</label>
              <div class="col-sm-9">
                <input type="password" class="form-control" v-model="passwordModel.confirmPass" placeholder="Nhập lại mật khẩu">
              </div>
            </div>
            <div class="d-flex justify-content-end">
              <button type="button" class="btn btn-outline-secondary fw-bold" @click="onChangePass">Đổi mật khẩu</button>
            </div>
          </form>
        </div>
      </div>
    </section>
  </template>
  

    <script setup lang="ts">
    import { ref } from "vue";
    import { storeToRefs } from "pinia";
    import userInfoApi from "@/api/user-info.api";
    import { userStore } from "../stores/auth";
    import { useRouter } from "vue-router";

    const authStore = userStore();
    const { user } = storeToRefs(authStore);
    const router = useRouter();

    const userModel = ref({
        id: '',
        userName: '',
        fullName: '',
        email: '',
    })

    const passwordModel = ref({
        oldPass: '',
        newPass: '',
        confirmPass: '',
    })

    const init = () => {
        if (user.value) {
            userModel.value.email = user.value.email
            userModel.value.userName = user.value.username
            userModel.value.fullName = user.value.fullName
            userModel.value.id = user.value.userId
        }
    }

    const onUpdaterInfo = async () => {
        if (!userModel.value.userName) {
            return;
        }

        if (!userModel.value.fullName || !userModel.value.email) {
            alert("Vui lòng nhập đủ tên và email")
            return;
        }

        const response = await userInfoApi.updateUserInfo(userModel.value);
        if (response && response.data.result.isSuccess) {
            authStore.login({
                userId: +userModel.value.id,
                username: userModel.value.userName,
                email: userModel.value.email,
                fullName: userModel.value.fullName,
            });
            alert('Đã cập nhật thành công');
        } else {
            alert(response.data.result.message ?? 'Lỗi');
        }
    }

    const onChangePass = async () => {
        if (!passwordModel.value.oldPass
            || !passwordModel.value.newPass
            || !passwordModel.value.confirmPass) {
            alert("Vui lòng nhập đủ thông tin")
            return;
        }

        if (passwordModel.value.confirmPass != passwordModel.value.newPass) {
            alert("Mật khẩu mới và nhập lại không khớp")
            return;
        }

        const response = await userInfoApi.changePassword({
            userName: user.value.username,
            oldPassword: passwordModel.value.oldPass,
            newPassword: passwordModel.value.newPass
        });
        if (response && response.data.result.isSuccess) {
            authStore.logout()
            alert('Đã cập nhật mật khẩu thành công bạn sẽ được chuyển đến trang đăng nhập');
            router.push("/login");
        } else {
            alert(response.data.result.message ?? 'Lỗi');
        }
    }

    init()
    </script>