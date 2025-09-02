import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

import { type TLoginSchema, useLoginSchema } from "../schemas/login-schema";

interface IUseLoginHook {
  handleSubmit: (data: TLoginSchema) => Promise<void>;
}

export const useLoginHook = ({ handleSubmit }: IUseLoginHook) => {
  // const { t } = useTranslation("form", { keyPrefix: "login" });
  const loginSchema = useLoginSchema();
  const form = useForm<TLoginSchema>({
    resolver: zodResolver(loginSchema),
    mode: "onSubmit",
    defaultValues: {
      username_or_email: "",
      password: "",
      remember: false,
    },
  });

  return {
    form,
    onSubmit: form.handleSubmit(handleSubmit),
  };
};
