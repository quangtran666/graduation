import { Link } from "@tanstack/react-router";
import { Loader2 } from "lucide-react";
import { type ComponentProps } from "react";
import { useTranslation } from "react-i18next";

import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { cn } from "@/lib/utils";

import { useForgotPasswordHook } from "../hooks/forgot-password-hook";

export function ForgotPasswordForm({ className, ...properties }: ComponentProps<"form">) {
  const { t } = useTranslation("form");
  const { form, formProperties, isLoading, isSuccess } = useForgotPasswordHook();

  if (isSuccess) {
    return (
      <div className={cn("flex flex-col gap-6", className)}>
        <div className="flex flex-col items-center gap-2 text-center">
          <h1 className="text-2xl font-bold">{t("forgotPassword.title")}</h1>
          <p className="text-muted-foreground text-sm text-balance">
            {t("forgotPassword.success")}
          </p>
        </div>
        <div className="flex flex-col gap-4">
          <Button asChild className="w-full">
            <Link to="/login">{t("common.backTo", { page: t("login.pageTitle") })}</Link>
          </Button>
          <Button variant="outline" onClick={() => globalThis.location.reload()} className="w-full">
            Send another email
          </Button>
        </div>
      </div>
    );
  }

  return (
    <Form {...form}>
      <form {...formProperties} {...properties} className={cn("flex flex-col gap-6", className)}>
        <div className="flex flex-col items-center gap-2 text-center">
          <h1 className="text-2xl font-bold">{t("forgotPassword.title")}</h1>
          <p className="text-muted-foreground text-sm text-balance">
            {t("forgotPassword.description")}
          </p>
        </div>
        <div className="grid gap-6">
          <FormField
            control={form.control}
            name="email"
            render={({ field }) => (
              <FormItem className="grid gap-3">
                <FormLabel>{t("forgotPassword.email")}</FormLabel>
                <FormControl>
                  <Input type="email" placeholder="m@example.com" {...field} variant="clean" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button type="submit" className="w-full" disabled={isLoading}>
            {isLoading ? <Loader2 className="animate-spin" /> : t("forgotPassword.resetPassword")}
          </Button>
        </div>
        <div className="text-center text-sm">
          {t("forgotPassword.rememberPassword")}{" "}
          <Link to="/login" className="underline underline-offset-4">
            {t("common.backTo", { page: t("login.pageTitle") })}
          </Link>
        </div>
      </form>
    </Form>
  );
}
