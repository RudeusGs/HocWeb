import baseApi from "./base.api";

export default {
  getAllOrder: async () => {
    return await baseApi.get('Order/get-by-user-id?');
  },
};
