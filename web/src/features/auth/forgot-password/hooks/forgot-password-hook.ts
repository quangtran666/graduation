import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

import {
  type TForgotPasswordSchema,
  useForgotPasswordSchema,
} from "../schemas/forgot-password-schema";

interface IUseForgotPasswordHook {
  handleSubmit: (_data: TForgotPasswordSchema) => void;
}

export const useForgotPasswordHook = ({ handleSubmit }: IUseForgotPasswordHook) => {
  const forgotPasswordSchema = useForgotPasswordSchema();
  const form = useForm<TForgotPasswordSchema>({
    resolver: zodResolver(forgotPasswordSchema),
    mode: "onSubmit",
    defaultValues: {
      email: "",
    },
  });

  return {
    form,
    onSubmit: form.handleSubmit(handleSubmit),
  };
};
