import { useTranslation } from "react-i18next";
import { z } from "zod";

export const useLoginSchema = () => {
  const { t } = useTranslation("form");

  const LOGIN_SCHEMA = z.object({
    email: z
      .email({
        error: t("common.form.required", { field: t("common.form.name.email") }),
      })
      .max(50, { error: t("common.form.max", { length: 50 }) }),
    password: z
      .string({
        error: t("common.form.required", { field: t("common.form.name.password") }),
      })
      .min(6, { error: t("common.form.min", { length: 6 }) }),
  });

  return LOGIN_SCHEMA;
};

export type TLoginSchema = z.infer<ReturnType<typeof useLoginSchema>>;
