import Cookies from "js-cookie";

import { DEFAULT_LOCALE, type SupportedLocale } from "@/constants/locale";

const LOCALE_COOKIE_KEY = "locale";
const COOKIE_EXPIRES = 365;
export const COOKIE_MINUTES = 365 * 24 * 60;

export const getLocaleCookie = (): SupportedLocale => {
  const cookieValue = Cookies.get(LOCALE_COOKIE_KEY);
  return (cookieValue as SupportedLocale) || DEFAULT_LOCALE;
};

export const setLocaleCookie = (locale: SupportedLocale): void => {
  Cookies.set(LOCALE_COOKIE_KEY, locale, {
    expires: COOKIE_EXPIRES,
    sameSite: "lax",
    secure: globalThis.location.protocol === "https:",
  });
};

export const removeLocaleCookie = (): void => {
  Cookies.remove(LOCALE_COOKIE_KEY);
};
