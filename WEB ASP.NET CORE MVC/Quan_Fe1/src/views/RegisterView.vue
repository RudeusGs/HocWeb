<template>
    <section class="container">
    <div class="row justify-content-center mb-5">
      <div class="col-md-6 text-center">
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-6 col-lg-4">
        <div class="login-wrap p-4 p-md-5">
          <h3 class="mb-4 text-center">Đăng ký</h3>
          <form  class="signin-form">
          <div class="form-group mb-3">
              <input type="text" class="form-control" placeholder="Tên hiển thị" required>
            </div>
            <div class="form-group mb-3">
              <input type="text" class="form-control" placeholder="Tên tài khoản" required>
            </div>
            <div class="form-group mb-3">
              <input type="text" class="form-control" placeholder="Mật khẩu" required>
            </div>
            <div class="form-group mb-3">
              <input type="text" class="form-control" placeholder="Nhập lại mật khẩu" required>
            </div>
            <div class="form-group mb-3 position-relative">
              <input id="password-field" type="password" class="form-control" placeholder="Email" required>
              <span class="fa fa-fw fa-eye field-icon toggle-password"></span>
            </div>
            <div class="form-group">
              <button type="submit" class="form-control btn btn-primary submit px-3">Đăng ký</button>
            </div>
            <div class="form-group d-flex justify-content-between">
              <div class="w-100 text-end">
                <a href="/login" style="color: #000">Quay lại đăng nhập</a>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>
<style scoped>
    .login-wrap {
  background: #f7f7f7;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}
</style>
