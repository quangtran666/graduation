import type { UserInfo } from "@/api/common/userinfo";

export interface LoginResponse {
  message: string;
  user: UserInfo;
}
