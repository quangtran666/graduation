import { useMutation } from "@tanstack/react-query";
import { useRouter } from "@tanstack/react-router";
import { toast } from "sonner";

import { logoutEndpoint } from "@/api/auth/logout/endpoint";
import { useCurrentUser } from "@/hooks/use-current-user";

export const useLogoutHook = () => {
  const router = useRouter();
  const { clearUser } = useCurrentUser();

  const logoutMutation = useMutation({
    mutationFn: logoutEndpoint,
    onSuccess: (data) => {
      clearUser();
      toast.success(data.message);
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
