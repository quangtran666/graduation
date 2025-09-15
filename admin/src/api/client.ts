import axios, { type AxiosError, type AxiosResponse } from "axios";

import { excludeEndpoints } from "@/api/constants";
import { HTTP_STATUS } from "@/constants/http";

import { REFRESH_ERRORS } from "./error";
import { tokenManager } from "./single-flight-token-manager";
import type { ExtendedAxiosRequestConfig } from "./types";

export const baseApiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL as string,
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

void baseApiClient.interceptors.response.use(
  (response: AxiosResponse) => response,
  async (error: AxiosError) => {
    const originalRequest = error.config as ExtendedAxiosRequestConfig;
    const isExcludedEndpoint = excludeEndpoints.some((endpoint) =>
      originalRequest?.url?.includes(endpoint),
    );

    if (
      error.response?.status === HTTP_STATUS.UNAUTHORIZED &&
      originalRequest &&
      !originalRequest.retry &&
      !isExcludedEndpoint
    ) {
      originalRequest.retry = true;

      const refreshResult = await tokenManager.refreshToken();

      const result = await refreshResult.match(
        async () => await baseApiClient(originalRequest),
        (refreshError) => {
          tokenManager.reset();
          const errorMessage = {
            [REFRESH_ERRORS.REFRESH_FAILED]: "Authentication expired. Please login again.",
            [REFRESH_ERRORS.ALREADY_REFRESHING]: "Multiple refresh attempts detected.",
            [REFRESH_ERRORS.NETWORK_ERROR]: "Network error during token refresh.",
          }[refreshError];
          throw new Error(errorMessage);
        },
      );

      return result;
    }

    throw error;
  },
);

export const apiClient = baseApiClient;
