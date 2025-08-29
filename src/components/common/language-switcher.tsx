"use client";

import { useLocaleManager } from "@/hooks/use-locale-manager";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "../ui/dropdown-menu";
import { Button } from "../ui/button";
import { Languages, Loader2 } from "lucide-react";
import {
  getLocaleFlag,
  getLocaleLabel,
  LOCALE_CONFIG,
  SUPPORTED_LOCALES,
  SupportedLocale,
} from "@/constants/locale";

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
          <span className="ml-2">{LOCALE_CONFIG[locale as SupportedLocale].label}</span>
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
              <span className="mr-2">{getLocaleFlag(supportedLocale)}</span>
              {getLocaleLabel(supportedLocale)}
            </DropdownMenuItem>
          ))}
        </div>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
