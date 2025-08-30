"use client";
import { Button } from "@/components/ui/button";
import { LOGIN_ROUTE } from "@/constants/route";
import { authClient } from "@/lib/auth/auth-client";
import { useRouter } from "next/navigation";

export default function Home() {
  const router = useRouter();
  const { data } = authClient.useSession();
  return (
    <>
      <Button
        onClick={async () =>
          await authClient.signOut({
            fetchOptions: {
              onSuccess: () => {
                router.replace(LOGIN_ROUTE);
              },
            },
          })
        }
      >
        Sign out
      </Button>
      <pre>{JSON.stringify(data, null, 2)}</pre>
    </>
  );
}
