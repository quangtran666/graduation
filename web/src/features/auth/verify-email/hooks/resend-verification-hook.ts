import { resendVerification } from "@/api/auth/resend-verification/endpoint";
import type { ApiError } from "@/api/error";
import { HTTP_STATUS } from "@/constants/http";
import { VERIFY_EMAIL_COOLDOWN_SECONDS } from "@/features/auth/verify-email/constants";
import { useMutation } from "@tanstack/react-query";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";

export const useResendVerificationHook = (email: string) => {
  const { t } = useTranslation("form");
  const [cooldownSeconds, setCooldownSeconds] = useState(0);
  const [isOnCooldown, setIsOnCooldown] = useState(false);

  useEffect(() => {
    let interval: NodeJS.Timeout;

    if (cooldownSeconds > 0) {
      setIsOnCooldown(true);
      interval = setInterval(() => {
        setCooldownSeconds((prev) => {
          if (prev <= 1) {
            setIsOnCooldown(false);
            return 0;
          }
          return prev - 1;
        });
      }, 1000);
    }

    return () => clearInterval(interval);
  }, [cooldownSeconds]);

  const resendMutation = useMutation({
    mutationFn: resendVerification,
    onSuccess: (data) => {
      toast.success(data.message);
      setCooldownSeconds(data.cooldownSeconds);
    },
    onError: (error: ApiError) => {
      switch (error.response?.status) {
        case HTTP_STATUS.NOT_FOUND:
          toast.error(t("verifyEmail.error.userNotFound"));
          break;
        case HTTP_STATUS.CONFLICT:
          toast.error(t("verifyEmail.error.waitingPeriodOrAlreadyVerified"));
          setCooldownSeconds(VERIFY_EMAIL_COOLDOWN_SECONDS);
          break;
        default:
          toast.error(t("verifyEmail.error.failed"));
      }
    },
  });

  const handleResend = () => {
    if (isOnCooldown || !email) return;
    resendMutation.mutate({ email });
  };

  return {
    handleResend,
    isLoading: resendMutation.isPending,
    isOnCooldown,
    cooldownSeconds,
  };
};
