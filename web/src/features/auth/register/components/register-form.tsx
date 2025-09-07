import { Link } from "@tanstack/react-router";
import { Loader2 } from "lucide-react";
import { type ComponentProps } from "react";
import { useTranslation } from "react-i18next";

import { GithubIcon } from "@/components/icon/github";
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

import { useRegisterHook } from "../hooks/register-hook";

export function RegisterForm({ className, ...properties }: ComponentProps<"form">) {
  const { t } = useTranslation("form");
  const { form, onSubmit, isLoading } = useRegisterHook();

  return (
    <Form {...form}>
      <form onSubmit={onSubmit} {...properties} className={cn("flex flex-col gap-6", className)}>
        <div className="flex flex-col items-center gap-2 text-center">
          <h1 className="text-2xl font-bold">{t("register.title")}</h1>
          <p className="text-muted-foreground text-sm text-balance">{t("register.description")}</p>
        </div>
        <div className="grid gap-4">
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
            {isLoading ? <Loader2 className="animate-spin" /> : t("register.signUp")}
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
            <Link to="/login" className="underline underline-offset-4">
              {t("common.signIn")}
            </Link>
          </div>
        </div>
      </form>
    </Form>
  );
}
