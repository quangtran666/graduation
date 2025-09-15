import type { AxiosRequestConfig } from "axios";

export interface ExtendedAxiosRequestConfig extends AxiosRequestConfig {
  retry?: boolean;
}
