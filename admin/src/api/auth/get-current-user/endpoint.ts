import { apiClient } from "@/api/client";

import { type GetCurrentUserResponse } from "./response";

export const getCurrentUserEndpoint = async (): Promise<GetCurrentUserResponse> => {
  const response = await apiClient.get<GetCurrentUserResponse>("/api/auth/me");
  return response.data;
};
