import { createAsyncThunk } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

type FileUploadResponse = {
  imageId: string;
};

export const uploadFile = createAsyncThunk("uploadFile", async (file: File) => {
  console.log("inside thunk of upload");
  try {
    const formData = new FormData();
    formData.append("file", file);
    const response = await axios.post<FileUploadResponse>(
      "http://localhost:5145/api/v1/images/upload",
      formData
    );
    console.log("response", response);
    console.log("response data", response.data);
    return response.data;
  } catch (e) {
    const error = e as AxiosError;
    console.log("Error uploading file:", error);
    throw error;
  }
});
