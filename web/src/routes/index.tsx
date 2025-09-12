import { createFileRoute, Link } from "@tanstack/react-router";
import { LogOut, User } from "lucide-react";

import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { useLogoutHook } from "@/features/auth/logout/hooks/logout-hook";
import { getUserInfo, isAuthenticated } from "@/lib/cookie/auth-cookie";

export const Route = createFileRoute("/")({
  component: RouteComponent,
});

function RouteComponent() {
  const userInfo = getUserInfo();
  const authenticated = isAuthenticated();
  const { logout, isLoading } = useLogoutHook();

  if (!authenticated) {
    return (
      <div className="flex min-h-screen items-center justify-center">
        <Card className="w-full max-w-md">
          <CardHeader className="text-center">
            <CardTitle>Welcome!</CardTitle>
            <CardDescription>You are not logged in</CardDescription>
          </CardHeader>
          <CardContent className="space-y-4">
            <Link to="/login">
              <Button className="w-full">Sign In</Button>
            </Link>
            <Link to="/register">
              <Button variant="outline" className="w-full">
                Create Account
              </Button>
            </Link>
          </CardContent>
        </Card>
      </div>
    );
  }

  return (
    <div className="flex min-h-screen items-center justify-center">
      <Card className="w-full max-w-md">
        <CardHeader className="text-center">
          <CardTitle className="flex items-center justify-center gap-2">
            <User className="h-5 w-5" />
            Welcome back!
          </CardTitle>
          <CardDescription>You are successfully logged in</CardDescription>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="space-y-2">
            <div className="text-sm">
              <strong>Username:</strong> {userInfo?.username}
            </div>
            <div className="text-sm">
              <strong>Email:</strong> {userInfo?.email}
            </div>
            <div className="text-sm">
              <strong>Email Verified:</strong>{" "}
              <span className={userInfo?.emailVerified ? "text-green-600" : "text-amber-600"}>
                {userInfo?.emailVerified ? "Yes" : "No"}
              </span>
            </div>
            <div className="text-sm">
              <strong>User ID:</strong> {userInfo?.id}
            </div>
          </div>
          <Button onClick={logout} disabled={isLoading} variant="outline" className="w-full">
            <LogOut className="mr-2 h-4 w-4" />
            {isLoading ? "Logging out..." : "Logout"}
          </Button>
        </CardContent>
      </Card>
    </div>
  );
}
