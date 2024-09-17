import baseApi from "./base.api";

export default {
  getOrderDetail: async (orderId: number| string) => {
    return await baseApi.get("OrderDetail/get-by-oder-id?oderId=" +orderId);
  },
};
