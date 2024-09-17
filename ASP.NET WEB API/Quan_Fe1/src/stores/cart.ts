import type { Product } from "@/models/interface";
import { defineStore } from "pinia";

export const useCartStore = defineStore({
  id: "cart",
  state: () => ({
    items: [] as Product[],
  }),
  actions: {
    loadCart() {
      const cs = localStorage.getItem("cart");
      if (!cs) this.items = [];
      else this.items = JSON.parse(cs);
    },
    addToCart(product: Product) {
      const item = this.items.find((x) => x.id == product.id);
      if (item) {
        item.quantity += +product.quantity;
      } else {
        this.items.push(product);
      }
      localStorage.setItem("cart", JSON.stringify(this.items));
    },
    removeCartItem(id: number) {
      this.items = this.items.filter((item) => item.id !== id);
      localStorage.setItem("cart", JSON.stringify(this.items));
    },
    clearCart() {
      this.items = [];
      localStorage.setItem("cart", JSON.stringify(this.items));
    },
    clearSelectedCart() {
      this.items = this.items.filter((item) => !item.checked);
      localStorage.setItem("cart", JSON.stringify(this.items));
    },
    cartTotal() {
      let total = 0;
      this.items.forEach(x=>{
        total+= x.quantity*x.unitPrice
      })
      return total;
    },
    updateCheck(){
      localStorage.setItem("cart", JSON.stringify(this.items));
    },
    chosedTotal() {
      let total = 0;
      this.items.filter(x=>x.checked).forEach(x=>{
        total+= x.quantity*x.unitPrice
      })
      return total;
    },
  },
});
