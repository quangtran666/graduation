import type React from "react";
import { useTranslation } from "react-i18next";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { cn } from "@/lib/utils";

export function LoginForm({ className, ...properties }: React.ComponentProps<"form">) {
  const { t } = useTranslation("form");

  return (
    <form className={cn("flex flex-col gap-6", className)} {...properties}>
      <div className="flex flex-col items-center gap-2 text-center">
        <h1 className="text-2xl font-bold">{t("login.pageTitle")}</h1>
        <p className="text-muted-foreground text-sm text-balance">{t("login.description")}</p>
      </div>
      <div className="grid gap-6">
        <div className="grid gap-3">
          <Label htmlFor="email">{t("login.email")}</Label>
          <Input id="email" type="email" placeholder="admin@example.com" required />
        </div>
        <div className="grid gap-3">
          <Label htmlFor="password">{t("login.password")}</Label>
          <Input id="password" type="password" required />
        </div>
        <Button type="submit" className="w-full">
          {t("login.button")}
        </Button>
      </div>
    </form>
  );
}
