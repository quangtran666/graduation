import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";

import { AppSidebar } from "@/components/sidebar/app-sidebar";
import { SiteHeader } from "@/components/sidebar/site-header";
import { SidebarInset, SidebarProvider } from "@/components/ui/sidebar";
import { getCurrentUserQueryOptions } from "@/features/auth/user/query-options/me";

export const Route = createFileRoute("/dashboard")({
  beforeLoad: async ({ context }) => {
    if (!context.queryClient) return redirect({ to: "/login" });

    await context.queryClient.ensureQueryData(getCurrentUserQueryOptions).catch(() => {
      redirect({ to: "/login", search: { redirect: location.href }, throw: true });
    });
  },
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <SidebarProvider
      style={
        {
          "--sidebar-width": "calc(var(--spacing) * 72)",
          "--header-height": "calc(var(--spacing) * 12)",
        } as React.CSSProperties
      }
    >
      <AppSidebar variant="inset" />
      <SidebarInset>
        <SiteHeader />
        <Outlet />
      </SidebarInset>
    </SidebarProvider>
  );
}
