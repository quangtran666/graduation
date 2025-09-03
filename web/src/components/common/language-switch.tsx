import { Languages, Loader2 } from "lucide-react";
import { useState } from "react";
import { useTranslation } from "react-i18next";

import {
  getLocaleFlag,
  getLocaleLabel,
  LOCALE_CONFIG,
  SUPPORTED_LOCALES,
  type SupportedLocale,
} from "@/constants/locale";
import { setLocaleCookie } from "@/lib/cookie/locale-cookie";

import { Button } from "../ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "../ui/dropdown-menu";

export function LanguageSwitch() {
  const { i18n } = useTranslation();
  const [isChanging, setIsChanging] = useState(false);

  const changeLocale = async (locale: SupportedLocale) => {
    if (locale === i18n.language || isChanging) return;

    setIsChanging(true);
    try {
      await i18n.changeLanguage(locale);
      setLocaleCookie(locale);
    } catch (error) {
      console.error("Failed to change language:", error);
    } finally {
      setIsChanging(false);
    }
  };

  const currentLocale = i18n.language as SupportedLocale;

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="outline" size="sm" disabled={isChanging}>
          {isChanging ? (
            <Loader2 className="h-4 w-4 animate-spin" />
          ) : (
            <Languages className="h-4 w-4" />
          )}
          <span className="ml-2">
            {LOCALE_CONFIG[currentLocale]?.label || getLocaleLabel(currentLocale)}
          </span>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end">
        <div className="flex flex-col gap-1">
          {SUPPORTED_LOCALES.map((supportedLocale) => (
            <DropdownMenuItem
              key={supportedLocale}
              onClick={() => changeLocale(supportedLocale)}
              className={i18n.language === supportedLocale ? "bg-accent" : ""}
              disabled={isChanging}
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
