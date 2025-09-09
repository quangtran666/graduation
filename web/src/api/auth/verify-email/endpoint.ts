import type { VerifyEmailRequest } from "@/api/auth/verify-email/request";
import type { VerifyEmailResponse } from "@/api/auth/verify-email/response";
import { apiClient } from "@/api/client";

export const verifyEmail = async (data: VerifyEmailRequest): Promise<VerifyEmailResponse> => {
  const response = await apiClient.post<VerifyEmailResponse>(
    `/api/auth/verify-email?token=${data.token}`,
  );
  return response.data;
};
