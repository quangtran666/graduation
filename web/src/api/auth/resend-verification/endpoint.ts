import { apiClient } from "../../client";
import type { ResendVerificationRequest } from "./request";
import type { ResendVerificationResponse } from "./response";

export const resendVerification = async (
  data: ResendVerificationRequest,
): Promise<ResendVerificationResponse> => {
  const response = await apiClient.post<ResendVerificationResponse>(
    "/api/auth/resend-verification",
    data,
  );
  return response.data;
};
