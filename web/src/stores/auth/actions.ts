import { type StateCreator } from "zustand";

import { type UserInfo } from "@/api/common/userinfo";

import { type AuthActions, type AuthStore } from "./types";

export const createAuthActions: StateCreator<AuthStore, [], [], AuthActions> = (set) => ({
  login: (user: UserInfo) =>
    set(
      () => ({
        user,
        isAuthenticated: true,
      }),
      false,
    ),

  logout: () =>
    set(
      () => ({
        user: undefined,
        isAuthenticated: false,
      }),
      false,
    ),
});
