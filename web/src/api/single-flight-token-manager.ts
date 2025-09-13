import { err, ok, type Result } from "neverthrow";

import { refreshTokenEndpoint } from "@/api/auth/refresh/endpoint";

import { REFRESH_ERRORS, type RefreshError } from "./error";

export class SingleFlightTokenManager {
  private isRefreshing = false;
  private refreshPromise: Promise<Result<void, RefreshError>> | undefined;

  async refreshToken(): Promise<Result<void, RefreshError>> {
    if (this.isRefreshing && this.refreshPromise) return this.refreshPromise;

    this.isRefreshing = true;
    this.refreshPromise = this.performRefresh();

    const result = await this.refreshPromise;
    this.isRefreshing = false;
    this.refreshPromise = undefined;

    return result;
  }

  private async performRefresh(): Promise<Result<void, RefreshError>> {
    const refreshResult = await this.safeRefreshCall();

    return refreshResult.match(
      () => ok(),
      (error) => err(error),
    );
  }

  private async safeRefreshCall(): Promise<Result<void, RefreshError>> {
    try {
      await refreshTokenEndpoint();
      return ok();
    } catch (error) {
      if (error instanceof Error && error.message.includes("Network"))
        return err(REFRESH_ERRORS.NETWORK_ERROR);
      return err(REFRESH_ERRORS.REFRESH_FAILED);
    }
  }

  reset(): void {
    this.isRefreshing = false;
    this.refreshPromise = undefined;
  }
}

export const tokenManager = new SingleFlightTokenManager();
