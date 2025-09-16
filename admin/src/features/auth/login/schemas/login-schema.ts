import { useTranslation } from "react-i18next";
import { z } from "zod";

export const useLoginSchema = () => {
  const { t } = useTranslation("form");

  const LOGIN_SCHEMA = z.object({
    email: z.email({ error: t("login.validation.email") }),
    password: z
      .string({
        error: t("common.form.required", { field: t("common.form.name.password") }),
      })
      .min(6, { error: t("common.form.min", { length: 6 }) }),
  });

  return LOGIN_SCHEMA;
};

export type TLoginSchema = z.infer<ReturnType<typeof useLoginSchema>>;
