export const LOCALE_CONFIG = {
  en: {
    label: "English",
    flag: "🇺🇸",
  },
  vi: {
    label: "Tiếng Việt",
    flag: "🇻🇳",
  },
};

export type SupportedLocale = keyof typeof LOCALE_CONFIG;
export const SUPPORTED_LOCALES = Object.keys(LOCALE_CONFIG) as SupportedLocale[];
export const DEFAULT_LOCALE: SupportedLocale = "en";

export const getLocaleFlag = (locale: SupportedLocale) => LOCALE_CONFIG[locale].flag;
export const getLocaleLabel = (locale: SupportedLocale) => LOCALE_CONFIG[locale].label;
