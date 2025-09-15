import type { UserInfo } from "@/api/common/userinfo";

export interface GetCurrentUserResponse {
  message: string;
  user: UserInfo;
}
