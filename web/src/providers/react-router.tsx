import { createRouter, RouterProvider } from "@tanstack/react-router";
import { useTranslation } from "react-i18next";

import { routeTree } from "@/routeTree.gen";

const router = createRouter({
  routeTree,
  context: {
    i18n: undefined,
  },
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

export const TanstackRouterProvider = () => {
  const { i18n } = useTranslation(["auth", "metadata", "form"]);

  return <RouterProvider router={router} context={{ i18n }} />;
};
