import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

import {
  type TResetPasswordSchema,
  useResetPasswordSchema,
} from "../schemas/reset-password-schema";

interface IUseResetPasswordHook {
  handleSubmit: (_data: TResetPasswordSchema) => void;
}

export const useResetPasswordHook = ({ handleSubmit }: IUseResetPasswordHook) => {
  const resetPasswordSchema = useResetPasswordSchema();
  const form = useForm<TResetPasswordSchema>({
    resolver: zodResolver(resetPasswordSchema),
    mode: "onSubmit",
    defaultValues: {
      password: "",
      confirmPassword: "",
    },
  });

  return {
    form,
    onSubmit: form.handleSubmit(handleSubmit),
  };
};
