import type { QueryClient } from "@tanstack/react-query";
import { createRootRouteWithContext, HeadContent, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import { Toaster } from "sonner";

import i18n from "@/lib/i18n/i18n";

interface RouterContext {
  i18n: typeof i18n | undefined;
  queryClient: QueryClient | undefined;
}

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootComponent,
  notFoundComponent: () => <div>Not found!</div>,
});

function RootComponent() {
  return (
    <>
      <HeadContent />
      <Outlet />
      <TanStackRouterDevtools />
      <Toaster /> {/* Todo: Move this to better location */}
    </>
  );
}
