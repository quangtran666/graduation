import { createFileRoute } from "@tanstack/react-router";

import { useLogoutHook } from "@/features/auth/logout/hooks/logout-hook";

export const Route = createFileRoute("/")({
  component: RouteComponent,
});

function RouteComponent() {
  const { handleLogout } = useLogoutHook();
  return <div onClick={handleLogout}>logout</div>;
}
