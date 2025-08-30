import { EMAIL_PATTERN_REGEX, NO_SPACES_REGEX } from "@/constants/regex";
import { useTranslations } from "next-intl";
import { z } from "zod";

export const useRegisterSchema = () => {
  const t = useTranslations();

  const REGISTER_SCHEMA = z
    .object({
      username: z
        .string()
        .min(3, { error: t("form.common.form.min", { length: 3 }) })
        .max(20, { error: t("form.common.form.max", { length: 20 }) })
        .refine((v) => !NO_SPACES_REGEX.test(v), {
          error: t("form.register.validation.usernameNoSpaces"),
        })
        .refine((v) => !EMAIL_PATTERN_REGEX.test(v), {
          error: t("form.common.form.emailNoAt", { field: t("form.common.form.name.username") }),
        }),
      email: z.email({ error: t("form.register.validation.email") }),
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
      error: t("form.register.validation.passwordMismatch"),
      path: ["confirmPassword"],
    });

  return REGISTER_SCHEMA;
};

export type TRegisterSchema = z.infer<ReturnType<typeof useRegisterSchema>>;
