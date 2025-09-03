import i18n from "i18next";
import LanguageDetector, { type DetectorOptions } from "i18next-browser-languagedetector";
import resourcesToBackend from "i18next-resources-to-backend";
import { initReactI18next } from "react-i18next";

import { DEFAULT_LOCALE, SUPPORTED_LOCALES, type SupportedLocale } from "@/constants/locale";
import { COOKIE_MINUTES, getLocaleCookie, setLocaleCookie } from "@/lib/cookie/locale-cookie";

const detectorOptions: DetectorOptions = {
  order: ["cookie"],
  caches: ["cookie"],
  cookieMinutes: COOKIE_MINUTES,
  cookieOptions: {
    path: "/",
    sameSite: "lax",
    secure: globalThis.location.protocol === "https:",
  },
};

const initialLanguage = getLocaleCookie();

i18n
  .use(
    resourcesToBackend(
      (language: string, namespace: string) => import(`@/locales/${language}/${namespace}.json`),
    ),
  )
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    fallbackLng: DEFAULT_LOCALE,
    supportedLngs: SUPPORTED_LOCALES,
    react: { useSuspense: true },
    lng: initialLanguage,
    interpolation: {
      escapeValue: false,
    },
    detection: detectorOptions,
  });

i18n.on("languageChanged", (lng) => {
  setLocaleCookie(lng as SupportedLocale);
});

export { default } from "i18next";
