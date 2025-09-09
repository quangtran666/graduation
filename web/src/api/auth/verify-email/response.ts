import type { UserInfo } from "@/api/common/userinfo";

export interface VerifyEmailResponse {
  message: string;
  user: UserInfo;
}
