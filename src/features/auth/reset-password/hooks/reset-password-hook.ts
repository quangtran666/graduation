import { useForm } from "react-hook-form";
import { TResetPasswordSchema, useResetPasswordSchema } from "../schemas/reset-password-schema";
import { zodResolver } from "@hookform/resolvers/zod";

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
