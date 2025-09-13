import "@/App.css";

import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { createRoot } from "react-dom/client";
import { I18nextProvider } from "react-i18next";

import { THEME_STORAGE_KEY } from "@/constants/common";
import i18n from "@/lib/i18n/i18n";
import { QueryRouterProvider } from "@/providers/query-router-provider";
import { ThemeProvider } from "@/providers/theme/theme-provider";

const rootElement = document.querySelector("#root");
if (!rootElement) throw new Error('Root element "#root" not found');

if (!rootElement.hasChildNodes()) {
  const root = createRoot(rootElement);
  root.render(
    <ThemeProvider defaultTheme="system" storageKey={THEME_STORAGE_KEY}>
      <I18nextProvider i18n={i18n}>
        <QueryRouterProvider>
          <ReactQueryDevtools />
        </QueryRouterProvider>
      </I18nextProvider>
    </ThemeProvider>,
  );
}
