import type { RegisterRequest } from "@/api/auth/register/request";
import type { RegisterResponse } from "@/api/auth/register/response";
import { apiClient } from "@/api/client";

export const registerUser = async (data: RegisterRequest): Promise<RegisterResponse> => {
  const response = await apiClient.post<RegisterResponse>("/api/auth/register", data);
  return response.data;
};
