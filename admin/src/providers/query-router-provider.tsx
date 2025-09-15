import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { createRouter, RouterProvider } from "@tanstack/react-router";
import type { PropsWithChildren } from "react";
import { useTranslation } from "react-i18next";

import { routeTree } from "@/routeTree.gen";

const queryClient = new QueryClient();

const router = createRouter({
  routeTree,
  context: {
    i18n: undefined,
    queryClient: undefined,
  },
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

export const QueryRouterProvider = ({ children }: PropsWithChildren) => {
  const { i18n } = useTranslation(["auth", "metadata", "form"]);

  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router} context={{ i18n, queryClient }} />
      {children}
    </QueryClientProvider>
  );
};
