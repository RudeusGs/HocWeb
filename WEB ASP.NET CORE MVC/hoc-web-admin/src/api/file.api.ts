import baseApi from './base.api';

export default {
    getImage: async (img: string) => {
      return await baseApi.getPublicFile(img);
    },
};