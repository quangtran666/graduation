import { useSuspenseQuery } from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";

import { useLogoutHook } from "@/features/auth/logout/hooks/logout-hook";
import { getCurrentUserQueryOptions } from "@/features/auth/user/query-options/me";

export const Route = createFileRoute("/")({
  component: RouteComponent,
  loader: ({ context }) => {
    if (!context.queryClient) throw new Error("Query client not found in context");
    // Todo: Pretty sure cái này mình sẽ cần load ở cái layout to hơn, vì cái này ở những cái nhỏ hơn sẽ sử dụng rất nhiều
    return context.queryClient.ensureQueryData(getCurrentUserQueryOptions);
  },
});

function RouteComponent() {
  const { handleLogout } = useLogoutHook();
  const { data: currentUser } = useSuspenseQuery(getCurrentUserQueryOptions);

  return (
    <div className="space-y-4 p-6">
      <h1 className="text-2xl font-bold">Thông tin User hiện tại</h1>

      <div className="rounded-lg bg-gray-100 p-4">
        <pre className="text-sm whitespace-pre-wrap">
          {JSON.stringify(currentUser, undefined, 2)}
        </pre>
      </div>

      <button
        onClick={handleLogout}
        className="rounded bg-red-500 px-4 py-2 text-white hover:bg-red-600"
      >
        Logout
      </button>
    </div>
  );
}
