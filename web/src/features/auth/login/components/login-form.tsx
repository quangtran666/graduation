import { Link } from "@tanstack/react-router";
import { Loader2 } from "lucide-react";
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

import { useLoginHook } from "../hooks/login-hook";

export function LoginForm({ className, ...properties }: React.ComponentProps<"form">) {
  const { t } = useTranslation("form");
  const { form, formProperties, isLoading } = useLoginHook();

  return (
    <Form {...form}>
      <form {...formProperties} {...properties} className={cn("flex flex-col gap-6", className)}>
        <div className="flex flex-col items-center gap-2 text-center">
          <h1 className="text-2xl font-bold">{t("login.title")}</h1>
          <p className="text-muted-foreground text-sm text-balance">{t("login.description")}</p>
        </div>
        <div className="grid gap-4">
          <FormField
            control={form.control}
            name="username_or_email"
            render={({ field }) => (
              <FormItem className="grid gap-3">
                <FormLabel>{t("login.usernameOrEmail")}</FormLabel>
                <FormControl>
                  <Input {...field} variant="clean" />
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
                <div className="flex items-center">
                  <FormLabel>{t("login.password")}</FormLabel>
                  <Link
                    to="/forgot-password"
                    className="ml-auto text-sm underline-offset-4 hover:underline"
                  >
                    {t("login.forgotPassword")}
                  </Link>
                </div>
                <FormControl>
                  <Input type="password" {...field} variant="clean" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button type="submit" className="w-full" disabled={isLoading}>
            {isLoading ? <Loader2 className="size-4 animate-spin" /> : t("common.signIn")}
          </Button>
          <div className="after:border-border relative text-center text-sm after:absolute after:inset-0 after:top-1/2 after:z-0 after:flex after:items-center after:border-t">
            <span className="bg-background text-muted-foreground relative z-10 px-2">
              {t("common.orContinueWith")}
            </span>
          </div>
          <Button variant="outline" className="w-full">
            <GithubIcon />
            {t("common.signInWith", { provider: "GitHub" })}
          </Button>
        </div>
        <div className="text-center text-sm">
          {t("login.noAccount")}{" "}
          <Link to="/register" className="underline underline-offset-4">
            {t("login.signUp")}
          </Link>
        </div>
      </form>
    </Form>
  );
}
