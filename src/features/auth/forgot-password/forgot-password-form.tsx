import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { getTranslations } from "next-intl/server";
import Link from "next/link";
import { ComponentProps } from "react";

export async function ForgotPasswordForm({ className, ...props }: ComponentProps<"form">) {
  const t = await getTranslations("form");

  return (
    <form className={cn("flex flex-col gap-6", className)} {...props}>
      <div className="flex flex-col items-center gap-2 text-center">
        <h1 className="text-2xl font-bold">{t("forgotPassword.title")}</h1>
        <p className="text-muted-foreground text-sm text-balance">
          {t("forgotPassword.description")}
        </p>
      </div>
      <div className="grid gap-6">
        <div className="grid gap-3">
          <Label htmlFor="email">Email</Label>
          <Input id="email" type="email" placeholder="m@example.com" required />
        </div>
        <Button type="submit" className="w-full">
          {t("forgotPassword.resetPassword")}
        </Button>
      </div>
      <div className="text-center text-sm">
        {t("forgotPassword.rememberPassword")}{" "}
        <Link href="/login" className="underline underline-offset-4">
          {t("common.backTo", { page: t("login.pageTitle") })}
        </Link>
      </div>
    </form>
  );
}
