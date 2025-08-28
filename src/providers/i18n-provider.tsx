import { NextIntlClientProvider } from "next-intl";
import { PropsWithChildren } from "react";

export async function I18nProvider({ children }: PropsWithChildren) {
  return <NextIntlClientProvider>{children}</NextIntlClientProvider>;
}
