export const LOCALE_CONFIG = {
  en: {
    label: "English",
    flag: "🇺🇸",
    dir: "ltr",
  },
  vi: {
    label: "Tiếng Việt",
    flag: "🇻🇳",
    dir: "ltr",
  },
} as const;

// Drived values from LOCALE_CONFIG
export type SupportedLocale = keyof typeof LOCALE_CONFIG;
export const SUPPORTED_LOCALES: readonly SupportedLocale[] = Object.keys(
  LOCALE_CONFIG,
) as SupportedLocale[];
export const DEFAULT_LOCALE: SupportedLocale = "en";

// Constants
export const LOCALE_COOKIE_NAME = "NEXT_LOCALE";
export const LOCALE_COOKIE_EXPIRES = Date.now() + 365 * 24 * 60 * 60 * 1000; // 1 year

// Helper functions
export function getLocaleLabel(locale: SupportedLocale) {
  return LOCALE_CONFIG[locale].label;
}

export function getLocaleFlag(locale: SupportedLocale) {
  return LOCALE_CONFIG[locale].flag;
}

export function getLocaleDir(locale: SupportedLocale) {
  return LOCALE_CONFIG[locale].dir;
}
