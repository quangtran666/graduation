import { createFileRoute, Link } from "@tanstack/react-router";
import { Shield } from "lucide-react";

import { LanguageSwitch } from "@/components/common/language-switch";
import { ModeToggle } from "@/components/common/theme-switch";
import { LoginForm } from "@/features/login/components/login-form";

export const Route = createFileRoute("/(auth)/login")({
  head: ({ match }) => ({
    meta: [
      {
        title: match.context.i18n?.t("metadata:login.title"),
      },
    ],
  }),
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <div className="grid min-h-svh lg:grid-cols-2">
      <div className="flex flex-col gap-4 p-6 md:p-10">
        <div className="flex justify-center gap-2 md:justify-between">
          <Link to="/login" className="flex items-center gap-2 font-medium">
            <div className="bg-primary text-primary-foreground flex size-6 items-center justify-center rounded-md">
              <Shield className="size-4" />
            </div>
            Admin Portal
          </Link>
          <div className="flex items-center gap-2">
            <LanguageSwitch />
            <ModeToggle />
          </div>
        </div>
        <div className="flex flex-1 items-center justify-center">
          <div className="w-full max-w-xs">
            <LoginForm />
          </div>
        </div>
      </div>
      <div className="bg-muted relative hidden lg:block">
        <img
          src="/secure-admin-dashboard-interface.jpg"
          alt="Admin Dashboard"
          className="absolute inset-0 h-full w-full object-cover dark:brightness-[0.2] dark:grayscale"
        />
      </div>
    </div>
  );
}
