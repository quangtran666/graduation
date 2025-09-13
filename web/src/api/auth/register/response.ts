import type { UserInfo } from "@/api/common/userinfo";

export interface RegisterResponse {
  message: string;
  user: UserInfo;
}
