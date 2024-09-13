import baseApi from "./base.api";

export default {
  getProductById: async (id: number| string) => {
    return await baseApi.get("Product/get-by-id?id=" +id);
  },
  getAllProduct: async () => {
    return await baseApi.get("Product/get-all");
  },
};
