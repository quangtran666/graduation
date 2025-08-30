import {
  Body,
  Button,
  Container,
  Head,
  Heading,
  Hr,
  Html,
  Link,
  Preview,
  Section,
  Text,
  Tailwind,
} from "@react-email/components";
import { APP_NAME, SUPPORT_EMAIL } from "@/constants/common";
import { getTranslations } from "next-intl/server";

export type VerificationEmailProps = {
  url: string;
  appName?: string;
  supportEmail?: string;
};

export default async function VerificationEmail({
  url,
  appName = APP_NAME,
  supportEmail = SUPPORT_EMAIL,
}: VerificationEmailProps) {
  const t = await getTranslations("auth.email.verification");
  return (
    <Html>
      <Head />
      <Preview>{t("preview", { appName })}</Preview>
      <Tailwind>
        <Body className="bg-gray-50 font-sans">
          <Container className="my-10 w-[480px] max-w-full rounded border border-gray-200 bg-white px-6 py-8">
            <Heading className="m-0 text-xl font-semibold text-black">Verify your email</Heading>
            <Text className="mt-2 text-sm text-gray-700">{t("description", { appName })}</Text>

            <Section className="my-8 text-center">
              <Button
                href={url}
                className="inline-flex cursor-pointer items-center justify-center rounded bg-black px-5 py-3 text-[14px] font-medium text-white no-underline"
              >
                {t("verifyEmail")}
              </Button>
            </Section>

            <Text className="text-xs text-gray-700">{t("linkExpired")}</Text>
            <Link href={url} className="text-xs break-all text-blue-600">
              {url}
            </Link>

            <Hr className="my-8 border-gray-200" />

            <Text className="text-xs leading-5 text-gray-500">
              {t("contactSupport", { supportEmail })}
            </Text>
          </Container>
        </Body>
      </Tailwind>
    </Html>
  );
}
