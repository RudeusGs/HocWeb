import baseApi from "./base.api";

export default {
  updateUserInfo: async (payload: any) => {
    return await baseApi.post("UserManagement/update-info", payload);
  },
  changePassword: async (payload: any) => {
    return await baseApi.post("UserManagement/change-password", payload);
  },
};
