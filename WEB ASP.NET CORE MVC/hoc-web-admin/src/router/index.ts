import { createRouter, createWebHistory } from "vue-router";
import Layout from "@/layout/AdminLayout.vue";
import LoginView from "@/views/LoginView.vue";
import DashboardView from "@/views/DashboardView.vue";
import NotFound from "@/views/NotFound.vue";
import CategoryView from "@/views/CategoryView.vue";
import AccountView from "@/views/AccountView.vue";
import ProductView from "@/views/ProductView.vue";
import OrderView from "@/views/OrderView.vue";
import Cookies from 'js-cookie'

const routeItem = {
  path: "",
  component: Layout,
  children: [
    {
      path: "",
      name: "dashboard",
      component: DashboardView,
    },
    {
      path: "category",
      name: "category",
      component: CategoryView,
    },
    {
      path: "user",
      name: "user",
      component: AccountView,
    },
    {
      path: "product",
      name: "product",
      component: ProductView,
    },
    {
      path: "order",
      name: "order",
      component: OrderView,
    },
  ],
  meta: { requiresAdmin: true },
};

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { ...routeItem },
    {
      path: "/login",
      name: "login",
      component: LoginView,
    },
    {
      path: "/:pathMatch(.*)*",
      name: "not-found",
      component: NotFound,
    },
  ],
});

function isAdmin() {
  const token =  Cookies.get('token')
  if (token) {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload.role.includes('Admin');
  }
  return false;
}

router.beforeEach((to, from, next) => {

  if (to.matched.some(record => record.meta.requiresAdmin)) {
    if (isAdmin()) {
      next();
    } else {
      Cookies.remove("token");
      localStorage.removeItem('user');
      next('/login');
    }
  } else {
    next();
  }
});

export default router;