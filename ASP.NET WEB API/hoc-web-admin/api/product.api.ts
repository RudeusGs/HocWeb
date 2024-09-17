import baseApi from "./base.api";

export default {
  getAllProduct: async () => {
    return await baseApi.get("Product/get-all");
  },
  getCategoryById: async (id: number| string) => {
    return await baseApi.get("Category/get-by-id?id=" +id);
  },
};
