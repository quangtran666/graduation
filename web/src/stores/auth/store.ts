import { create } from "zustand";
import { devtools, persist } from "zustand/middleware";

import { createAuthActions } from "./actions";
import { authPersistConfig } from "./persistent";
import { initialAuthState } from "./state";
import { type AuthStore } from "./types";

export const useAuthStore = create<AuthStore>()(
  devtools(
    persist(
      (set, get, api) => ({
        ...initialAuthState,
        ...createAuthActions(set, get, api),
      }),
      authPersistConfig,
    ),
    {
      name: "auth-store",
    },
  ),
);
