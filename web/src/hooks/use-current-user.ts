import { useQuery, useQueryClient } from "@tanstack/react-query";

import type { GetCurrentUserResponse } from "@/api/auth/get-current-user/response";
import { USER_QUERY_KEYS } from "@/features/auth/user/query-keys";
import { getCurrentUserQueryOptions } from "@/features/auth/user/query-options/me";

export const useCurrentUser = () => {
  const queryClient = useQueryClient();

  const {
    data: user,
    isLoading,
    error,
  } = useQuery({
    ...getCurrentUserQueryOptions,
    retry: false,
  });

  const isAuthenticated = !!user && !error;

  const setUser = (newUser: GetCurrentUserResponse) => {
    queryClient.setQueryData(USER_QUERY_KEYS.current, newUser);
  };

  const clearUser = () => {
    queryClient.setQueryData(USER_QUERY_KEYS.current, undefined);
    queryClient.removeQueries({ queryKey: USER_QUERY_KEYS.current });
  };

  return {
    user,
    isAuthenticated,
    isLoading,
    error,
    setUser,
    clearUser,
  };
};
