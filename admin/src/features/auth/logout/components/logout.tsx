import { IconLoader2, IconLogout } from "@tabler/icons-react";

import { Button } from "@/components/ui/button";
import { DEFAULT_ROUTE_API } from "@/constants/router";
import { useLogoutHook } from "@/features/auth/logout/hooks/logout-hook";

export function Logout() {
  const { queryClient } = DEFAULT_ROUTE_API.useRouteContext();
  const { handleLogout, isLoading } = useLogoutHook({ queryClient });

  return (
    <Button onClick={handleLogout} disabled={isLoading} size="full" className="justify-start">
      {isLoading ? <IconLoader2 className="animate-spin" /> : <IconLogout />}
      Log out
    </Button>
  );
}
