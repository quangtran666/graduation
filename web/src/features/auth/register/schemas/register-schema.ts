import { useTranslation } from "react-i18next";
import { z } from "zod";

import { EMAIL_PATTERN_REGEX, NO_SPACES_REGEX } from "@/constants/regex";

export const useRegisterSchema = () => {
  const { t } = useTranslation("form");

  const REGISTER_SCHEMA = z
    .object({
      username: z
        .string()
        .min(3, { error: t("common.form.min", { length: 3 }) })
        .max(20, { error: t("common.form.max", { length: 20 }) })
        .refine((v) => !NO_SPACES_REGEX.test(v), {
          error: t("register.validation.usernameNoSpaces"),
        })
        .refine((v) => !EMAIL_PATTERN_REGEX.test(v), {
          error: t("common.form.emailNoAt", { field: t("common.form.name.username") }),
        }),
      email: z.email({ error: t("register.validation.email") }),
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
      error: t("register.validation.passwordMismatch"),
      path: ["confirmPassword"],
    });

  return REGISTER_SCHEMA;
};

export type TRegisterSchema = z.infer<ReturnType<typeof useRegisterSchema>>;
