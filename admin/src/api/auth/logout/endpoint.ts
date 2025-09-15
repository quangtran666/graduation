import { apiClient } from "@/api/client";

import { type LogoutResponse } from "./response";

export const logoutEndpoint = async (): Promise<LogoutResponse> => {
  const response = await apiClient.post<LogoutResponse>("/api/auth/logout");
  return response.data;
};
