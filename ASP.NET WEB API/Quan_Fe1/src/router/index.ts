import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import About from '@/views/AboutView.vue'
import LoginView from '@/views/LoginView.vue'
import ProductView from '@/views/ProductView.vue'
import ProductDetail from '@/views/ProductDetail.vue'
import RegisterView from '@/views/RegisterView.vue'
import UserInfoView from '@/views/UserInfoView.vue'
import CartView from '@/views/CartView.vue'
import CheckoutView from '@/views/CheckoutView.vue'
import Order from '@/views/OrderView.vue'
import { userStore } from '../stores/auth'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/about',
    name: 'about',
    component: About
  },
  {
    path: '/product/:categoryId?',
    name: 'product',
    component: ProductView
  },
  {
    path: '/product-detail/:id?',
    name: 'product-detail',
    component: ProductDetail
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterView
  },
  {
    path: '/infor_use',
    name: 'infor_use',
    component: UserInfoView
  },
  {
    path: '/cartview', 
    name: 'cartview',
    component: CartView, 
    meta: { requiresAuth: true }
  },
  {
    path: '/checkoutview',
    name: 'checkoutview',
    component: CheckoutView
  },
  {
    path: '/order',
    name: 'order',
    component: Order
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  const authStore = userStore();
  if (to.meta.requiresAuth && !authStore.user) {
    next('/login');
    alert("Không vào được đâu!");
  } else {
    next();
  }
});

export default router;
