import { type User } from "better-auth";
import { resend } from "../mail/resend";
import { getTranslations } from "next-intl/server";
import VerificationEmail from "@/components/mail/auth/verification-email";

interface EmailVerificationData {
  user: User;
  url: string;
  token: string;
}

export const sendEmailVerification = async (
  data: EmailVerificationData,
  _request: Request | undefined,
) => {
  const t = await getTranslations("auth.email.verification");
  const { data: dataEmail, error } = await resend.emails.send({
    from: "Acme <onboarding@resend.dev>",
    to: data.user.email,
    subject: t("title"),
    react: VerificationEmail({ url: data.url }),
  });
  if (error) console.error(error);

  console.log(dataEmail);
};
