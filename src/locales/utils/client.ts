import "client-only";
import Cookies from "js-cookie";
import { type Locale } from "next-intl";
import { LOCALE_COOKIE_NAME, DEFAULT_LOCALE, SUPPORTED_LOCALES } from "../constant";
import { LOCALE_COOKIE_EXPIRES } from "../constant";

export function getClientLocale(): Locale {
  const locale = Cookies.get(LOCALE_COOKIE_NAME) as Locale;

  return SUPPORTED_LOCALES.includes(locale) ? locale : DEFAULT_LOCALE;
}

export function setClientLocale(locale: Locale): void {
  if (!SUPPORTED_LOCALES.includes(locale)) {
    throw new Error(`Unsupported locale: ${locale}`);
  }

  Cookies.set(LOCALE_COOKIE_NAME, locale, {
    expires: LOCALE_COOKIE_EXPIRES,
    path: "/",
    sameSite: "lax",
    secure: process.env.NODE_ENV === "production",
  });

  window.location.reload();
}
