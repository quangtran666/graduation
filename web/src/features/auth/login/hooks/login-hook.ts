import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";

import { type TLoginSchema, useLoginSchema } from "../schemas/login-schema";

interface IUseLoginHook {
  handleSubmit: (data: TLoginSchema) => Promise<void>;
}

export const useLoginHook = ({ handleSubmit }: IUseLoginHook) => {
  const { t } = useTranslation("form");
  const loginSchema = useLoginSchema();
  const form = useForm<TLoginSchema>({
    resolver: zodResolver(loginSchema),
    mode: "onSubmit",
    defaultValues: {
      username_or_email: "",
      password: "",
    },
  });

  return {
    form,
    onSubmit: form.handleSubmit(handleSubmit),
  };
};
