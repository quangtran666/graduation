import { getRequestConfig } from "next-intl/server";

export default getRequestConfig(async () => {
  const locale = "en";

  return {
    locale,
    messages: {
      form: (await import(`../locales/${locale}/form.json`)).default,
      metadata: (await import(`../locales/${locale}/metadata.json`)).default,
    },
  };
});
