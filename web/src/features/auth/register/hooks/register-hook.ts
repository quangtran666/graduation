import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

import { type TRegisterSchema, useRegisterSchema } from "../schemas/register-schema";

interface IUseRegisterHook {
  handleSubmit: (_data: TRegisterSchema) => void;
}

export const useRegisterHook = ({ handleSubmit }: IUseRegisterHook) => {
  const registerSchema = useRegisterSchema();
  const form = useForm<TRegisterSchema>({
    resolver: zodResolver(registerSchema),
    mode: "onSubmit",
    defaultValues: {
      username: "",
      email: "",
      password: "",
      confirmPassword: "",
    },
  });

  return {
    form,
    onSubmit: form.handleSubmit(handleSubmit),
  };
};
