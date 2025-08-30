import { useForm } from "react-hook-form";
import { TForgotPasswordSchema, useForgotPasswordSchema } from "../schemas/forgot-password-schema";
import { zodResolver } from "@hookform/resolvers/zod";

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
