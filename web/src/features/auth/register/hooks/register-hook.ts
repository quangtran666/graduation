import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "@tanstack/react-router";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";

import { registerUser } from "@/api/auth/register/endpoint";
import type { ApiError } from "@/api/error";
import { HTTP_STATUS } from "@/constants/http";

import { type TRegisterSchema, useRegisterSchema } from "../schemas/register-schema";

export const useRegisterHook = () => {
  const router = useRouter();
  const registerSchema = useRegisterSchema();
  const { t } = useTranslation("form");

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

  const registerMutation = useMutation({
    mutationFn: registerUser,
    onSuccess: (data) => {
      toast.success(data.message);
      void router.navigate({ to: "/verify-email", search: { email: data.user.email } });
    },
    onError: (error: ApiError) => {
      switch (error.response?.status) {
        case HTTP_STATUS.CONFLICT: {
          toast.error(t("register.error.userExists"));
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

  const onSubmit = (data: TRegisterSchema) => {
    registerMutation.mutate(data);
  };

  const formProperties = {
    onSubmit: form.handleSubmit(onSubmit),
  };

  return {
    form,
    formProperties,
    isLoading: registerMutation.isPending,
  };
};
