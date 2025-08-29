"use client";

import "client-only";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { ComponentProps } from "react";
import { useRegisterHook } from "../hooks/register-hook";
import { useTranslations } from "next-intl";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { GithubIcon } from "@/components/icons/github";
import { TRegisterSchema } from "../schemas/register-schema";
import { authClient } from "@/lib/auth-client";
import { Loader2 } from "lucide-react";
import { toast } from "sonner";
import { useRouter } from "next/navigation";

export function RegisterForm({ className, ...props }: ComponentProps<"form">) {
  const t = useTranslations("form");
  const router = useRouter();
  const { form, onSubmit } = useRegisterHook({
    handleSubmit: async (data: TRegisterSchema) => {
      await authClient.signUp.email(
        {
          email: data.email,
          name: data.username,
          password: data.password,
          username: data.username,
        },
        {
          onSuccess: (_ctx) => {
            toast.success(t("register.success"));
            router.replace("/login");
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
          <h1 className="text-2xl font-bold">{t("register.title")}</h1>
          <p className="text-muted-foreground text-sm text-balance">{t("register.description")}</p>
        </div>
        <div className="grid gap-6">
          <FormField
            control={form.control}
            name="username"
            render={({ field }) => (
              <FormItem className="grid gap-3">
                <FormLabel>{t("register.username")}</FormLabel>
                <FormControl>
                  <Input placeholder="John Doe" {...field} variant="clean" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="email"
            render={({ field }) => (
              <FormItem className="grid gap-3">
                <FormLabel>{t("register.email")}</FormLabel>
                <FormControl>
                  <Input placeholder="john.doe@example.com" {...field} variant="clean" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="password"
            render={({ field }) => (
              <FormItem className="grid gap-3">
                <FormLabel>{t("register.password")}</FormLabel>
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
                <FormLabel>{t("register.confirmPassword")}</FormLabel>
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
              t("register.signUp")
            )}
          </Button>
          <div className="after:border-border relative text-center text-sm after:absolute after:inset-0 after:top-1/2 after:z-0 after:flex after:items-center after:border-t">
            <span className="bg-background text-muted-foreground relative z-10 px-2">
              {t("common.orContinueWith")}
            </span>
          </div>
          <Button variant="outline" className="w-full bg-transparent">
            <GithubIcon />
            {t("common.signUpWith", { provider: "GitHub" })}
          </Button>
          <div className="text-center text-sm">
            {t("login.noAccount")}{" "}
            <a href="/login" className="underline underline-offset-4">
              {t("common.signIn")}
            </a>
          </div>
        </div>
      </form>
    </Form>
  );
}
