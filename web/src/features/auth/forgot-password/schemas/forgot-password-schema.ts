import { useTranslation } from "react-i18next";
import { z } from "zod";

export const useForgotPasswordSchema = () => {
  const { t } = useTranslation("form");

  const FORGOT_PASSWORD_SCHEMA = z.object({
    email: z.email({
      error: t("forgotPassword.validation.emailInvalid"),
    }),
  });

  return FORGOT_PASSWORD_SCHEMA;
};

export type TForgotPasswordSchema = z.infer<ReturnType<typeof useForgotPasswordSchema>>;
