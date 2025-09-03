import { useTranslation } from "react-i18next";
import { z } from "zod";

export const useResetPasswordSchema = () => {
  const { t } = useTranslation("form");

  const RESET_PASSWORD_SCHEMA = z
    .object({
      password: z
        .string({
          error: t("common.form.required", { field: t("common.form.name.password") }),
        })
        .min(8, { error: t("common.form.min", { length: 8 }) }),
      confirmPassword: z
        .string({
          error: t("common.form.required", {
            field: t("common.form.name.confirmPassword"),
          }),
        })
        .min(8, { error: t("common.form.min", { length: 8 }) }),
    })
    .refine((v) => v.password === v.confirmPassword, {
      error: t("resetPassword.validation.passwordMismatch"),
      path: ["confirmPassword"],
    });

  return RESET_PASSWORD_SCHEMA;
};

export type TResetPasswordSchema = z.infer<ReturnType<typeof useResetPasswordSchema>>;
