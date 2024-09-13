import { defineStore } from 'pinia';

export const useThemeStore = defineStore('theme', {
  state: () => ({
    theme: localStorage.getItem('light') ? 'light' : 'dark'
  }),
  actions: {
    toggleTheme() {
      this.theme = this.theme === 'dark' ? 'light' : 'dark';
      document.documentElement.setAttribute('data-bs-theme', this.theme);
      if (this.theme === 'light') {
        localStorage.setItem('light', 'set');
      } else {
        localStorage.removeItem('light');
      }
    },
    checkInitialTheme() {
      document.documentElement.setAttribute('data-bs-theme', this.theme);
    }
  }
});