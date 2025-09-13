import { apiClient } from "@/api/client";

import { type RefreshTokenResponse } from "./response";

export const refreshTokenEndpoint = async (): Promise<RefreshTokenResponse> => {
  const response = await apiClient.post<RefreshTokenResponse>("/api/auth/refresh");
  return response.data;
};
