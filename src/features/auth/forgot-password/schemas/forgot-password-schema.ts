import { useTranslations } from "next-intl";
import { z } from "zod";

export const useForgotPasswordSchema = () => {
  const t = useTranslations();

  const FORGOT_PASSWORD_SCHEMA = z.object({
    email: z.email({
      error: t("form.common.form.required", { field: t("form.common.form.name.email") }),
    }),
  });

  return FORGOT_PASSWORD_SCHEMA;
};

export type TForgotPasswordSchema = z.infer<ReturnType<typeof useForgotPasswordSchema>>;
