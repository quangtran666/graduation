import "@/App.css";

import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { I18nextProvider } from "react-i18next";

import { THEME_STORAGE_KEY } from "@/constants/common";
import i18n from "@/lib/i18n/i18n";
import { TanStackQueryClientProvider } from "@/providers/react-query";
import { TanstackRouterProvider } from "@/providers/react-router";
import { ThemeProvider } from "@/providers/theme/theme-provider";

const rootElement = document.querySelector("#root");
if (!rootElement) throw new Error('Root element "#root" not found');

if (!rootElement.hasChildNodes()) {
  const root = createRoot(rootElement);
  root.render(
    <StrictMode>
      <TanStackQueryClientProvider>
        <ThemeProvider defaultTheme="system" storageKey={THEME_STORAGE_KEY}>
          <I18nextProvider i18n={i18n}>
            <TanstackRouterProvider />
          </I18nextProvider>
        </ThemeProvider>
        <ReactQueryDevtools />
      </TanStackQueryClientProvider>
    </StrictMode>,
  );
}
