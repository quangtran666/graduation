import { type UserInfo } from "@/api/common/userinfo";

export interface AuthState {
  user: UserInfo | undefined;
  isAuthenticated: boolean;
}

export interface AuthActions {
  login: (user: UserInfo) => void;
  logout: () => void;
}

export type AuthStore = AuthState & AuthActions;
