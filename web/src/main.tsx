import "@/App.css";
import "@/lib/i18n/i18n";

import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { createRouter, RouterProvider as TanStackRouterProvider } from "@tanstack/react-router";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";

import { THEME_STORAGE_KEY } from "@/constants/common";
import { TanStackQueryClientProvider } from "@/providers/react-query";
import { ThemeProvider } from "@/providers/theme/theme-provider";

import { routeTree } from "./routeTree.gen";

const router = createRouter({ routeTree });

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

const rootElement = document.querySelector("#root");
if (!rootElement) throw new Error('Root element "#root" not found');

if (!rootElement.hasChildNodes()) {
  const root = createRoot(rootElement);
  root.render(
    <StrictMode>
      <TanStackQueryClientProvider>
        <ThemeProvider defaultTheme="system" storageKey={THEME_STORAGE_KEY}>
          <TanStackRouterProvider router={router} />
        </ThemeProvider>
        <ReactQueryDevtools />
      </TanStackQueryClientProvider>
    </StrictMode>,
  );
}
