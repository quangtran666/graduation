import type { AxiosError } from "axios";

export interface ApiErrorResponse {
  title: string;
  status: number;
  detail?: string;
}

export type ApiError = AxiosError<ApiErrorResponse>;
