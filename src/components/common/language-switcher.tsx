"use client";

import { useLocaleManager } from "@/hooks/use-locale-manager";
import { type Locale } from "next-intl";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "../ui/dropdown-menu";
import { Button } from "../ui/button";
import { Languages, Loader2 } from "lucide-react";
import { SUPPORTED_LOCALES } from "@/locales/constant";

const LOCALE_LABELS: Record<Locale, string> = {
  en: "English",
  vi: "Tiếng Việt",
};

export function LanguageSwitcher() {
  const { locale, isChanging, changeLocale } = useLocaleManager();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="outline" size="sm" disabled={isChanging}>
          {isChanging ? (
            <Loader2 className="h-4 w-4 animate-spin" />
          ) : (
            <Languages className="h-4 w-4" />
          )}
          <span className="ml-2">{LOCALE_LABELS[locale]}</span>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end">
        <div className="flex flex-col gap-1">
          {SUPPORTED_LOCALES.map((supportedLocale) => (
            <DropdownMenuItem
              key={supportedLocale}
              onClick={() => changeLocale(supportedLocale)}
              className={locale === supportedLocale ? "bg-accent" : ""}
            >
              <span className="mr-2">{supportedLocale === "en" ? "🇺🇸" : "🇻🇳"}</span>
              {LOCALE_LABELS[supportedLocale]}
            </DropdownMenuItem>
          ))}
        </div>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
