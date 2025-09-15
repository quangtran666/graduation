import { apiClient } from "@/api/client";

import { type LoginRequest } from "./request";
import { type LoginResponse } from "./response";

export const loginEndpoint = async (data: LoginRequest): Promise<LoginResponse> => {
  const response = await apiClient.post<LoginResponse>("/api/auth/login", data);
  return response.data;
};
