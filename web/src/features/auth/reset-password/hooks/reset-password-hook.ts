import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "@tanstack/react-router";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";

import { resetPassword } from "@/api/auth/reset-password/endpoint";
import type { ApiError } from "@/api/error";
import { HTTP_STATUS } from "@/constants/http";

import {
  type TResetPasswordSchema,
  useResetPasswordSchema,
} from "../schemas/reset-password-schema";

interface IUseResetPasswordHook {
  token?: string;
  onSuccess?: () => void;
}

export const useResetPasswordHook = ({ token, onSuccess }: IUseResetPasswordHook = {}) => {
  const { t } = useTranslation("form");
  const router = useRouter();
  const resetPasswordSchema = useResetPasswordSchema();
  const form = useForm<TResetPasswordSchema>({
    resolver: zodResolver(resetPasswordSchema),
    mode: "onSubmit",
    defaultValues: {
      password: "",
      confirmPassword: "",
    },
  });

  const mutation = useMutation({
    mutationFn: resetPassword,
    onSuccess: (data) => {
      toast.success(data.message);
      form.reset();
      onSuccess?.();
      void router.navigate({ to: "/login" });
    },
    onError: (error: ApiError) => {
      switch (error.response?.status) {
        case HTTP_STATUS.NOT_FOUND: {
          toast.error(t("resetPassword.validation.tokenInvalid"));
          break;
        }
        case HTTP_STATUS.CONFLICT: {
          toast.error(error.response?.data?.title || t("resetPassword.error"));
          break;
        }
        case HTTP_STATUS.BAD_REQUEST: {
          toast.error(t("resetPassword.error"));
          break;
        }
        default: {
          toast.error(t("resetPassword.error"));
          break;
        }
      }
    },
  });

  const onSubmit = (data: TResetPasswordSchema) => {
    if (!token) {
      toast.error(t("resetPassword.validation.tokenInvalid"));
      return;
    }
    mutation.mutate({
      token,
      newPassword: data.password,
      confirmPassword: data.confirmPassword,
    });
  };

  const formProperties = {
    onSubmit: form.handleSubmit(onSubmit),
  };

  return {
    form,
    formProperties,
    isLoading: mutation.isPending,
    isSuccess: mutation.isSuccess,
    hasValidToken: Boolean(token),
  };
};
