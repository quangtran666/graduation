/* eslint-disable react-hooks/exhaustive-deps */
import { useMutation } from "@tanstack/react-query";
import { getRouteApi } from "@tanstack/react-router";
import { useEffect } from "react";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";

import { verifyEmail } from "@/api/auth/verify-email/endpoint";
import type { ApiError } from "@/api/error";

const routeApi = getRouteApi("/(auth)/email-confirmed");

export const useEmailConfirmationHook = () => {
  const { t } = useTranslation("form");
  const { token } = routeApi.useSearch();

  const verifyEmailMutation = useMutation({
    mutationFn: verifyEmail,
    onSuccess: (data) => {
      toast.success(data.message);
    },
    onError: (error: ApiError) => {
      if (error.response) toast.error(t("emailConfirm.error.failed"));
    },
  });

  useEffect(() => {
    if (token && !verifyEmailMutation.isSuccess && !verifyEmailMutation.isError)
      verifyEmailMutation.mutate({ token });
  }, [token]);

  return {
    isLoading: verifyEmailMutation.isPending,
    isSuccess: verifyEmailMutation.isSuccess,
    isError: verifyEmailMutation.isError,
    error: verifyEmailMutation.error,
  };
};
