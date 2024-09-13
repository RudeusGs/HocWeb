import baseApi from "./base.api";

export default {
  createNewOrder: async (data:any) => {
    return await baseApi.post("Order/add-new", data);
  },
};
