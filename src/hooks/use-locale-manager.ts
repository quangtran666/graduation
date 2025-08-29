import "client-only";
import { setClientLocale } from "@/locales/utils/client";
import { type Locale, useLocale } from "next-intl";
import { useState } from "react";

export function useLocaleManager() {
  const currentLocale = useLocale();
  const [isChanging, setIsChanging] = useState(false);

  const changeLocale = (newLocale: Locale) => {
    if (newLocale === currentLocale) return;

    setIsChanging(true);
    setClientLocale(newLocale); // This will trigger a page reload
  };

  return {
    locale: currentLocale,
    isChanging,
    changeLocale,
  };
}
