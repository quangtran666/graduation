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

import { useResetPasswordHook } from "../hooks/reset-password-hook";

export function ResetPasswordForm({ className, ...properties }: ComponentProps<"form">) {
  const { t } = useTranslation("form");

  const { form, onSubmit } = useResetPasswordHook({
    handleSubmit: () => void Promise.resolve(),
  });

  return (
    <Form {...form}>
      <form onSubmit={onSubmit} {...properties} className={cn("flex flex-col gap-6", className)}>
        <div className="flex flex-col items-center gap-2 text-center">
          <h1 className="text-2xl font-bold">{t("resetPassword.title")}</h1>
          <p className="text-muted-foreground text-sm text-balance">
            {t("resetPassword.description")}
          </p>
        </div>
        <div className="grid gap-4">
          <FormField
            control={form.control}
            name="password"
            render={({ field }) => (
              <FormItem className="grid gap-3">
                <FormLabel>{t("resetPassword.password")}</FormLabel>
                <FormControl>
                  <Input type="password" {...field} variant="clean" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="confirmPassword"
            render={({ field }) => (
              <FormItem className="grid gap-3">
                <FormLabel>{t("resetPassword.confirmPassword")}</FormLabel>
                <FormControl>
                  <Input type="password" {...field} variant="clean" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button type="submit" className="w-full">
            {form.formState.isSubmitting ? (
              <Loader2 className="animate-spin" />
            ) : (
              t("resetPassword.resetPassword")
            )}
          </Button>
          <div className="text-center text-sm">
            {t("resetPassword.rememberPassword")}{" "}
            <a href="/login" className="underline underline-offset-4">
              {t("common.signIn")}
            </a>
          </div>
        </div>
      </form>
    </Form>
  );
}
