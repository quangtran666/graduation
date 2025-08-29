"use server";

import { cookies } from "next/headers";
import { type Locale } from "next-intl";
import {
  LOCALE_COOKIE_NAME,
  DEFAULT_LOCALE,
  SUPPORTED_LOCALES,
  LOCALE_COOKIE_EXPIRES,
} from "../constant";

export async function getServerLocale(): Promise<Locale> {
  const cookieStore = await cookies();
  const locale = cookieStore.get(LOCALE_COOKIE_NAME)?.value as Locale;

  return SUPPORTED_LOCALES.includes(locale) ? locale : DEFAULT_LOCALE;
}

export async function setServerLocale(locale: Locale): Promise<void> {
  const cookieStore = await cookies();

  if (!SUPPORTED_LOCALES.includes(locale)) {
    throw new Error(`Unsupported locale: ${locale}`);
  }

  cookieStore.set(LOCALE_COOKIE_NAME, locale, {
    expires: LOCALE_COOKIE_EXPIRES,
    path: "/",
    sameSite: "lax",
    secure: process.env.NODE_ENV === "production",
  });
}
