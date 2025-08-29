import { getRequestConfig } from "next-intl/server";
import { getServerLocale } from "./utils/server";

export default getRequestConfig(async () => {
  const locale = await getServerLocale();

  return {
    locale,
    messages: {
      form: (await import(`../locales/${locale}/form.json`)).default,
      metadata: (await import(`../locales/${locale}/metadata.json`)).default,
    },
  };
});
