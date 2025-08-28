import type { NextConfig } from "next";
import createNextIntlPlugin from "next-intl/plugin";

const nextConfig: NextConfig = {
  typedRoutes: true,
};

const withNextIntl = createNextIntlPlugin("./src/locales/request.ts");
export default withNextIntl(nextConfig);
