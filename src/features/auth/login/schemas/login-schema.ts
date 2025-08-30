import { EMAIL_PATTERN_REGEX, NO_SPACES_REGEX } from "@/constants/regex";
import { useTranslations } from "next-intl";
import { z } from "zod";

export const useLoginSchema = () => {
  const t = useTranslations();

  const LOGIN_SCHEMA = z.object({
    username_or_email: z
      .string()
      .min(3, { error: t("form.common.form.min", { length: 3 }) })
      .max(50, { error: t("form.common.form.max", { length: 50 }) })
      .refine(
        (v) => {
          if (EMAIL_PATTERN_REGEX.test(v)) return z.email().safeParse(v).success;
          return !NO_SPACES_REGEX.test(v) && v.length >= 3 && v.length <= 20;
        },
        {
          error: t("form.login.validation.usernameOrEmailInvalid"),
        },
      ),
    password: z
      .string({
        error: t("form.common.form.required", { field: t("form.common.form.name.password") }),
      })
      .min(6, { error: t("form.common.form.min", { length: 6 }) }),
    remember: z.boolean(),
  });

  return LOGIN_SCHEMA;
};

export type TLoginSchema = z.infer<ReturnType<typeof useLoginSchema>>;
