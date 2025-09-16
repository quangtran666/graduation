import type { GetCurrentUserResponse } from "@/api/auth/get-current-user/response";
import { apiClient } from "@/api/client";

export const getCurrentUserEndpoint = async (): Promise<GetCurrentUserResponse> => {
  const response = await apiClient.get<GetCurrentUserResponse>("/api/admin/auth/me");
  return response.data;
};
