import type { AxiosError } from "axios";

export interface ApiErrorResponse {
  title: string;
  status: number;
  detail?: string;
}

export type ApiError = AxiosError<ApiErrorResponse>;

export const REFRESH_ERRORS = {
  REFRESH_FAILED: "REFRESH_FAILED",
  ALREADY_REFRESHING: "ALREADY_REFRESHING",
  NETWORK_ERROR: "NETWORK_ERROR",
} as const;

export type RefreshError = "REFRESH_FAILED" | "ALREADY_REFRESHING" | "NETWORK_ERROR";
