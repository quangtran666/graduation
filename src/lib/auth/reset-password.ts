import { type User } from "better-auth";
import { getTranslations } from "next-intl/server";
import { resend } from "../mail/resend";
import ResetPasswordEmail from "@/components/mail/auth/reset-password-email";

interface ResetPasswordData {
  user: User;
  url: string;
  token: string;
}

export const resetPassword = async (data: ResetPasswordData, _request: Request | undefined) => {
  const t = await getTranslations("auth.email.resetPassword");
  const { error } = await resend.emails.send({
    from: "Acme <onboarding@resend.dev>",
    to: data.user.email,
    subject: t("title"),
    react: ResetPasswordEmail({ url: data.url }),
  });
  if (error) console.error(error);
};
