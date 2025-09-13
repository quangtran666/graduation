import { type PersistOptions } from "zustand/middleware";

import { type AuthStore } from "./types";

export const authPersistConfig: PersistOptions<AuthStore, Partial<AuthStore>> = {
  name: "auth-storage",
  partialize: (state) => ({
    user: state.user,
    isAuthenticated: state.isAuthenticated,
  }),
};
