import { Link } from "@tanstack/react-router";
import { CheckCircle, Loader2, XCircle } from "lucide-react";
import { useTranslation } from "react-i18next";

import { Button } from "@/components/ui/button";

import { useEmailConfirmationHook } from "../hooks/email-confirmation-hook";

export function EmailConfirmedForm() {
  const { t } = useTranslation("form");
  const { isLoading, isError } = useEmailConfirmationHook();

  if (isLoading) {
    return (
      <div className="flex flex-col gap-6">
        <div className="flex flex-col gap-2 text-center">
          <div className="mb-4 flex justify-center">
            <div className="rounded-full bg-blue-100 p-3 dark:bg-blue-900/20">
              <Loader2 className="size-8 animate-spin text-blue-600 dark:text-blue-400" />
            </div>
          </div>
          <h1 className="text-2xl font-bold">{t("emailConfirm.verifying.title")}</h1>
          <p className="text-muted-foreground text-balance">
            {t("emailConfirm.verifying.description")}
          </p>
        </div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="flex flex-col gap-6">
        <div className="flex flex-col gap-2 text-center">
          <div className="mb-4 flex justify-center">
            <div className="rounded-full bg-red-100 p-3 dark:bg-red-900/20">
              <XCircle className="size-8 text-red-600 dark:text-red-400" />
            </div>
          </div>
          <h1 className="text-2xl font-bold">{t("emailConfirm.error.title")}</h1>
          <p className="text-muted-foreground text-balance">
            {t("emailConfirm.error.description")}
          </p>
        </div>
        <div className="flex flex-col gap-4">
          <Button asChild className="w-full">
            <Link to="/login">{t("common.continueWith", { action: t("login.pageTitle") })}</Link>
          </Button>
        </div>
      </div>
    );
  }

  return (
    <div className="flex flex-col gap-6">
      <div className="flex flex-col gap-2 text-center">
        <div className="mb-4 flex justify-center">
          <div className="rounded-full bg-green-100 p-3 dark:bg-green-900/20">
            <CheckCircle className="size-8 text-green-600 dark:text-green-400" />
          </div>
        </div>
        <h1 className="text-2xl font-bold">{t("emailConfirm.success.title")}</h1>
        <p className="text-muted-foreground text-balance">
          {t("emailConfirm.success.description")}
        </p>
      </div>
      <div className="flex flex-col gap-4">
        <Button asChild className="w-full">
          <Link to="/login">{t("common.continueWith", { action: t("login.pageTitle") })}</Link>
        </Button>
        <Button variant="outline" asChild className="w-full bg-transparent">
          <Link to="/login">{t("common.gotohome")}</Link>
        </Button>
      </div>
    </div>
  );
}
