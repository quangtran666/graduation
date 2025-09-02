import i18n from "i18next";
import resourcesToBackend from "i18next-resources-to-backend";
import { initReactI18next } from "react-i18next";

i18n
  .use(
    resourcesToBackend(
      (language: string, namespace: string) => import(`@/locales/${language}/${namespace}.json`),
    ),
  )
  .use(initReactI18next)
  .init({
    fallbackLng: "en",
    supportedLngs: ["en", "vi"],
    react: { useSuspense: true },
    lng: "en",
    interpolation: {
      escapeValue: false,
    },
  });

export { default } from "i18next";
