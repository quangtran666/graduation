import type { ResetPasswordRequest } from "@/api/auth/reset-password/request";
import type { ResetPasswordResponse } from "@/api/auth/reset-password/response";
import { apiClient } from "@/api/client";

export const resetPassword = async (data: ResetPasswordRequest): Promise<ResetPasswordResponse> => {
  const response = await apiClient.post<ResetPasswordResponse>("/api/auth/reset-password", data);
  return response.data;
};
