import type { ForgotPasswordRequest } from "@/api/auth/forgot-password/request";
import type { ForgotPasswordResponse } from "@/api/auth/forgot-password/response";
import { apiClient } from "@/api/client";

export const forgotPassword = async (
  data: ForgotPasswordRequest,
): Promise<ForgotPasswordResponse> => {
  const response = await apiClient.post<ForgotPasswordResponse>("/api/auth/forgot-password", data);
  return response.data;
};
