import { createFileRoute, Link } from "@tanstack/react-router";
import { GalleryVerticalEnd } from "lucide-react";

import { LanguageSwitch } from "@/components/common/language-switch";
import { ModeToggle } from "@/components/common/theme-switch";
import { ResetPasswordForm } from "@/features/auth/reset-password/components/reset-password-form";

export const Route = createFileRoute("/(auth)/reset-password")({
  head: ({ match }) => ({
    meta: [
      {
        title: match.context.i18n?.t("metadata:resetPassword.title"),
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
              <GalleryVerticalEnd className="size-4" />
            </div>
            Acme Inc.
          </Link>
          <div className="flex items-center gap-2">
            <LanguageSwitch />
            <ModeToggle />
          </div>
        </div>
        <div className="flex flex-1 items-center justify-center">
          <div className="w-full max-w-xs">
            <ResetPasswordForm />
          </div>
        </div>
      </div>
      <div className="bg-muted relative hidden lg:block">
        <img
          src="/placeholder.svg"
          alt="Reset password illustration"
          className="object-cover dark:brightness-[0.2] dark:grayscale"
        />
      </div>
    </div>
  );
}
