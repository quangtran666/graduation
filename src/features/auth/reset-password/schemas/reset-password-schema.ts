import { useTranslations } from "next-intl";
import { z } from "zod";

export const useResetPasswordSchema = () => {
  const t = useTranslations();

  const RESET_PASSWORD_SCHEMA = z
    .object({
      password: z
        .string({
          error: t("form.common.form.required", { field: t("form.common.form.name.password") }),
        })
        .min(8, { error: t("form.common.form.min", { length: 8 }) }),
      confirmPassword: z
        .string({
          error: t("form.common.form.required", {
            field: t("form.common.form.name.confirmPassword"),
          }),
        })
        .min(8, { error: t("form.common.form.min", { length: 8 }) }),
    })
    .refine((v) => v.password === v.confirmPassword, {
      error: t("form.resetPassword.validation.passwordMismatch"),
      path: ["confirmPassword"],
    });

  return RESET_PASSWORD_SCHEMA;
};

export type TResetPasswordSchema = z.infer<ReturnType<typeof useResetPasswordSchema>>;
