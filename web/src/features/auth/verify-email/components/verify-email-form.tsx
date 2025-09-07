import { Mail, CheckCircle } from "lucide-react";

import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Link } from "@tanstack/react-router";

export function VerifyEmailForm() {
  return (
    <Card>
      <CardHeader className="text-center">
        <div className="mx-auto mb-4 flex size-12 items-center justify-center rounded-full bg-blue-100 dark:bg-blue-900">
          <Mail className="size-6 text-blue-600 dark:text-blue-400" />
        </div>
        <CardTitle className="text-xl">Check your email</CardTitle>
        <CardDescription className="text-balance">
          We've sent a verification link to your email address. Click the link to verify your
          account.
        </CardDescription>
      </CardHeader>
      <CardContent className="space-y-4">
        <div className="text-muted-foreground text-center text-sm">
          Didn't receive the email? Check your spam folder or try resending.
        </div>

        <Button
          //   onClick={handleResendEmail}
          //   disabled={isResending || resent}
          variant="outline"
          className="w-full bg-transparent"
        >
          {/* {isResending ? (
            "Sending..."
          ) : resent ? (
            <>
              <CheckCircle className="mr-2 size-4" />
              Email sent!
            </>
          ) : ( */}
          "Resend verification email"
          {/* )} */}
        </Button>

        <div className="text-center">
          <Link
            to="/login"
            className="text-muted-foreground hover:text-primary text-sm underline underline-offset-4"
          >
            Back to login
          </Link>
        </div>
      </CardContent>
    </Card>
  );
}
