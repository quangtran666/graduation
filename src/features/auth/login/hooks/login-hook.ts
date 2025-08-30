import { useForm } from "react-hook-form";
import { TLoginSchema, useLoginSchema } from "../schemas/login-schema";
import { zodResolver } from "@hookform/resolvers/zod";
import { EMAIL_PATTERN_REGEX } from "@/constants/regex";
import { authClient } from "@/lib/auth/auth-client";
import { toast } from "sonner";
import { useRouter } from "next/navigation";
import { HOME_ROUTE } from "@/constants/route";
import { useTranslations } from "next-intl";
import { SuccessContext, ErrorContext } from "better-auth/react";

interface IUseLoginHook {
  handleSubmit?: (data: TLoginSchema) => Promise<void>;
}

export const useLoginHook = ({ handleSubmit }: IUseLoginHook = {}) => {
  const router = useRouter();
  const t = useTranslations("form.login");
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

  const defaultOnSuccess = (_ctx: SuccessContext) => {
    router.replace(HOME_ROUTE);
    toast.success(t("success"));
  };

  const defaultOnError = (ctx: ErrorContext) => {
    toast.error(ctx.error.message);
  };

  const defaultSubmitHandler = async (data: TLoginSchema) => {
    const isEmail = EMAIL_PATTERN_REGEX.test(data.username_or_email);
    if (isEmail) {
      await authClient.signIn.email(
        {
          email: data.username_or_email,
          password: data.password,
          rememberMe: data.remember,
          callbackURL: HOME_ROUTE,
        },
        {
          onSuccess: defaultOnSuccess,
          onError: defaultOnError,
        },
      );
    } else {
      await authClient.signIn.username(
        {
          username: data.username_or_email,
          password: data.password,
          rememberMe: data.remember,
          callbackURL: HOME_ROUTE,
        },
        {
          onSuccess: defaultOnSuccess,
          onError: defaultOnError,
        },
      );
    }
  };

  return {
    form,
    onSubmit: form.handleSubmit(handleSubmit || defaultSubmitHandler),
  };
};
