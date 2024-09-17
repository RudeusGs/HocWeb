import axios from 'axios';
import Cookies from 'js-cookie'
import type { AxiosResponse } from 'axios';

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

const apiFile= axios.create({
  baseURL: import.meta.env.VITE_BASE_FILE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

function getToken() {
  return Cookies.get('token');
}

export default {
  get: async (endpoint: string, params?: Record<string, any>): Promise<AxiosResponse> => {
    const token = getToken();
    return await apiClient.get(endpoint, {
      headers: { Authorization: `Bearer ${token}` },
      params, 
    });
  },

  getPublicFile: async (endpoint: string): Promise<AxiosResponse> => {
    return await apiFile.get('public/' + encodeURIComponent(endpoint),{
      responseType: 'blob',
    });
  },

  // getFile: async (endpoint: string): Promise<AxiosResponse> => {
  //   const token = getToken();
  //   return await apiFile.get(encodeURIComponent(endpoint),{
  //     headers: { Authorization: `Bearer ${token}` },
  //     responseType: 'blob',
  //   });
  // },

  post: async ( endpoint: string, body: any): Promise<AxiosResponse> => {
    const token =  getToken();
    return await apiClient.post(`${endpoint}`, body, {
      headers: { Authorization: `Bearer ${token}` },
    });
  },

  postForm: async (endpoint: string, formData: FormData): Promise<AxiosResponse> => {
    const token = getToken();
    return await apiClient.post(endpoint, formData, {
      headers: { 
        Authorization: `Bearer ${token}`,
        'Content-Type': 'multipart/form-data'
      },
    });
  },

  postAuthenticate: async (endpoint: string, body: any): Promise<AxiosResponse> => {
    return await apiClient.post(`${endpoint}`, body);
  },

  put: async (endpoint: string, body: any): Promise<AxiosResponse> => {
    const token = getToken();
    return await apiClient.put(`${endpoint}`, body, {
      headers: { Authorization: `Bearer ${token}` },
    });
  },

  delete: async (endpoint: string): Promise<AxiosResponse> => {
    const token = getToken();
    return await apiClient.delete(`${endpoint}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  },
};