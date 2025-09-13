import { useTranslation } from "react-i18next";

import { Button } from "@/components/ui/button";
import { useResendVerificationHook } from "@/features/auth/verify-email/hooks/resend-verification-hook";

interface ResendButtonProperties {
  email: string;
}

export function ResendButton({ email }: ResendButtonProperties) {
  const { t } = useTranslation("form");
  const { handleResend, isLoading, isOnCooldown, cooldownSeconds } =
    useResendVerificationHook(email);

  return (
    <Button
      variant="outline"
      className="w-full bg-transparent"
      onClick={handleResend}
      disabled={isLoading || isOnCooldown}
    >
      {isOnCooldown
        ? t("verifyEmail.cooldown", { seconds: cooldownSeconds })
        : t("verifyEmail.resend")}
    </Button>
  );
}
