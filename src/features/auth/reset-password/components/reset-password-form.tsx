"use client";

import "client-only";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { ComponentProps } from "react";
import { useResetPasswordHook } from "../hooks/reset-password-hook";
import { useTranslations } from "next-intl";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { TResetPasswordSchema } from "../schemas/reset-password-schema";
import { authClient } from "@/lib/auth/auth-client";
import { Loader2 } from "lucide-react";
import { toast } from "sonner";
import { LOGIN_ROUTE } from "@/constants/route";
import { useRouter } from "next/navigation";

interface ResetPasswordFormProps extends ComponentProps<"form"> {
  searchParams: Record<string, string | string[] | undefined>;
}

export function ResetPasswordForm({ searchParams, className, ...props }: ResetPasswordFormProps) {
  const t = useTranslations("form");
  const router = useRouter();

  const { form, onSubmit } = useResetPasswordHook({
    handleSubmit: async (data: TResetPasswordSchema) => {
      const token = typeof searchParams.token === "string" ? searchParams.token : "";
      if (!token) {
        toast.error(t("resetPassword.validation.tokenInvalid"));
        return;
      }

      await authClient.resetPassword(
        {
          newPassword: data.password,
          token,
        },
        {
          onSuccess: () => {
            toast.success(t("resetPassword.success"));
            router.push(LOGIN_ROUTE);
          },
          onError: (ctx) => {
            toast.error(ctx.error.message);
          },
        },
      );
    },
  });

  return (
    <Form {...form}>
      <form onSubmit={onSubmit} {...props} className={cn("flex flex-col gap-6", className)}>
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
