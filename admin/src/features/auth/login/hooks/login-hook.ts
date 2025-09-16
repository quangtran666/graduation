import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "@tanstack/react-router";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";

import { loginEndpoint } from "@/api/auth/login/endpoint";
import type { ApiError } from "@/api/error";
import { HTTP_STATUS } from "@/constants/http";

import { type TLoginSchema, useLoginSchema } from "../schemas/login-schema";

export const useLoginHook = () => {
  const router = useRouter();
  const loginSchema = useLoginSchema();
  const { t } = useTranslation("form");

  const form = useForm<TLoginSchema>({
    resolver: zodResolver(loginSchema),
    mode: "onSubmit",
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const loginMutation = useMutation({
    mutationFn: loginEndpoint,
    onSuccess: (data) => {
      toast.success(data.message);
      void router.navigate({ to: "/" });
    },
    onError: (error: ApiError) => {
      switch (error.response?.status) {
        case HTTP_STATUS.UNAUTHORIZED: {
          toast.error(t("login.error"));
          break;
        }
        case HTTP_STATUS.FORBIDDEN: {
          if (error.response?.data?.detail) toast.warning(error.response.data.title);
          else toast.error(error.response?.data?.title || t("login.error"));
          break;
        }
        case HTTP_STATUS.BAD_REQUEST: {
          toast.error(t("login.error"));
          break;
        }
        default: {
          toast.error(error.response?.data?.title || t("login.error"));
          break;
        }
      }
    },
  });

  const handleSubmit = (data: TLoginSchema) => {
    loginMutation.mutate({
      email: data.email,
      password: data.password,
    });
  };

  const formProperties = {
    onSubmit: form.handleSubmit(handleSubmit),
  };

  return {
    form,
    formProperties,
    isLoading: loginMutation.isPending,
  };
};
