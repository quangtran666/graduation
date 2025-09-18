import { QueryClient, useMutation } from "@tanstack/react-query";
import { useRouter } from "@tanstack/react-router";
import { toast } from "sonner";

import { logoutEndpoint } from "@/api/auth/logout/endpoint";
import { getCurrentUserQueryOptions } from "@/features/auth/user/query-options/me";

interface LogoutHookProperties {
  queryClient?: QueryClient;
}

export const useLogoutHook = ({ queryClient }: LogoutHookProperties) => {
  const router = useRouter();

  const logoutMutation = useMutation({
    mutationFn: logoutEndpoint,
    onSuccess: (data) => {
      toast.success(data.message);
      queryClient?.removeQueries({ queryKey: getCurrentUserQueryOptions.queryKey });
      void router.navigate({ to: "/login" });
    },
    onError: (error) => {
      console.error("Logout error:", error);
      toast.error("Logout failed. Please try again.");
    },
  });

  const handleLogout = () => {
    logoutMutation.mutate();
  };

  return {
    handleLogout,
    isLoading: logoutMutation.isPending,
  };
};
