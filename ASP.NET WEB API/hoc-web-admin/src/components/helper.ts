import { rootFile } from "./contants";
import fileApi from "@/api/file.api";

export const getImage = async (filePath: string) => {
  // if (import.meta.env.MODE != "development") {
  //   return rootFile + filePath;
  // }
  const response2 = await fileApi.getImage(filePath);
  if (response2 && response2.data) {
    return URL.createObjectURL(response2.data);
  }
};
