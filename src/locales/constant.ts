import { type Locale } from "next-intl";

export const LOCALE_COOKIE_NAME = "NEXT_LOCALE";
export const DEFAULT_LOCALE: Locale = "en";
export const SUPPORTED_LOCALES: readonly Locale[] = ["en", "vi"] as const;
export const LOCALE_COOKIE_EXPIRES = Date.now() + 365 * 24 * 60 * 60 * 1000; // 1 year
