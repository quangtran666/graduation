import type { LogoutResponse } from "@/api/auth/logout/response";
import { apiClient } from "@/api/client";

export const logoutEndpoint = async (): Promise<LogoutResponse> => {
  const response = await apiClient.post<LogoutResponse>("/api/admin/auth/logout");
  return response.data;
};
