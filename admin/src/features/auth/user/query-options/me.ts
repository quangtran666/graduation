import { queryOptions } from "@tanstack/react-query";

import { getCurrentUserEndpoint } from "@/api/auth/get-current-user/endpoint";
import { DEFAULT_CACHE_TIME } from "@/constants/cache";
import { USER_QUERY_KEYS } from "@/constants/query-keys";

export const getCurrentUserQueryOptions = queryOptions({
  queryKey: USER_QUERY_KEYS.current,
  queryFn: getCurrentUserEndpoint,
  staleTime: DEFAULT_CACHE_TIME,
});
