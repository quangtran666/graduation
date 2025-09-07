import { Mail } from "lucide-react";

import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { getRouteApi, Link } from "@tanstack/react-router";
import { useTranslation } from "react-i18next";
import { ResendButton } from "@/features/auth/verify-email/components/resend-button";

const routeApi = getRouteApi("/(auth)/verify-email");

export function VerifyEmailForm() {
  const { t } = useTranslation("form");
  const { email } = routeApi.useSearch();

  return (
    <Card>
      <CardHeader className="text-center">
        <div className="bg-primary-foreground mx-auto mb-4 flex size-12 items-center justify-center rounded-full">
          <Mail className="text-primary size-6" />
        </div>
        <CardTitle className="text-xl">{t("verifyEmail.title")}</CardTitle>
        <CardDescription className="text-balance">{t("verifyEmail.description")}</CardDescription>
      </CardHeader>
      <CardContent className="space-y-4">
        <div className="text-muted-foreground text-center text-sm">
          {t("verifyEmail.resendTitle")}
        </div>

        <ResendButton email={email} />

        <div className="text-center">
          <Link
            to="/login"
            className="text-muted-foreground hover:text-primary text-sm underline underline-offset-4"
          >
            {t("verifyEmail.backToLogin")}
          </Link>
        </div>
      </CardContent>
    </Card>
  );
}
