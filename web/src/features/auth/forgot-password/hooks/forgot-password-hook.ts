import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";

import { forgotPassword } from "@/api/auth/forgot-password/endpoint";
import type { ApiError } from "@/api/error";
import { HTTP_STATUS } from "@/constants/http";

import {
  type TForgotPasswordSchema,
  useForgotPasswordSchema,
} from "../schemas/forgot-password-schema";

interface IUseForgotPasswordHook {
  onSuccess?: () => void;
}

export const useForgotPasswordHook = ({ onSuccess }: IUseForgotPasswordHook = {}) => {
  const { t } = useTranslation("form");
  const forgotPasswordSchema = useForgotPasswordSchema();
  const form = useForm<TForgotPasswordSchema>({
    resolver: zodResolver(forgotPasswordSchema),
    mode: "onSubmit",
    defaultValues: {
      email: "",
    },
  });

  const mutation = useMutation({
    mutationFn: forgotPassword,
    onSuccess: (data) => {
      toast.success(data.message);
      form.reset();
      onSuccess?.();
    },
    onError: (error: ApiError) => {
      switch (error.response?.status) {
        case HTTP_STATUS.NOT_FOUND: {
          toast.error(t("login.error"));
          break;
        }
        case HTTP_STATUS.CONFLICT: {
          toast.error(
            error.response?.data?.title || t("verifyEmail.error.waitingPeriodOrAlreadyVerified"),
          );
          break;
        }
        case HTTP_STATUS.BAD_REQUEST: {
          toast.error(t("register.error.invalidInput"));
          break;
        }
        default: {
          toast.error(t("register.error.failed"));
          break;
        }
      }
    },
  });

  const onSubmit = (data: TForgotPasswordSchema) => {
    mutation.mutate({ email: data.email });
  };

  const formProperties = {
    onSubmit: form.handleSubmit(onSubmit),
  };

  return {
    form,
    formProperties,
    isLoading: mutation.isPending,
    isSuccess: mutation.isSuccess,
  };
};
