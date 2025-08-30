"use client";

import "client-only";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { ComponentProps } from "react";
import { useForgotPasswordHook } from "../hooks/forgot-password-hook";
import { useTranslations } from "next-intl";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { TForgotPasswordSchema } from "../schemas/forgot-password-schema";
import { authClient } from "@/lib/auth/auth-client";
import { Loader2 } from "lucide-react";
import { toast } from "sonner";
import { RESET_PASSWORD_ROUTE } from "@/constants/route";
import Link from "next/link";
import { APP_URL } from "@/constants/common";

export function ForgotPasswordForm({ className, ...props }: ComponentProps<"form">) {
  const t = useTranslations("form");
  const { form, onSubmit } = useForgotPasswordHook({
    handleSubmit: async (data: TForgotPasswordSchema) => {
      const { data: result, error } = await authClient.requestPasswordReset({
        email: data.email,
        redirectTo: APP_URL + RESET_PASSWORD_ROUTE,
      });

      if (error) {
        toast.error(error.message);
        return;
      }

      if (result) toast.success(t("forgotPassword.success"));
    },
  });

  return (
    <Form {...form}>
      <form onSubmit={onSubmit} {...props} className={cn("flex flex-col gap-6", className)}>
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
          <Button type="submit" className="w-full" disabled={form.formState.isSubmitting}>
            {form.formState.isSubmitting ? (
              <Loader2 className="animate-spin" />
            ) : (
              t("forgotPassword.resetPassword")
            )}
          </Button>
        </div>
        <div className="text-center text-sm">
          {t("forgotPassword.rememberPassword")}{" "}
          <Link href="/login" className="underline underline-offset-4">
            {t("common.backTo", { page: t("login.pageTitle") })}
          </Link>
        </div>
      </form>
    </Form>
  );
}
