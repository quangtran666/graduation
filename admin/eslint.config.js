import js from "@eslint/js";
import globals from "globals";
import reactHooks from "eslint-plugin-react-hooks";
import reactRefresh from "eslint-plugin-react-refresh";
import tseslint from "typescript-eslint";
import { globalIgnores } from "eslint/config";
import eslintPluginUnicorn from "eslint-plugin-unicorn";
import eslintPluginReact from "eslint-plugin-react";
import eslintPluginPrettierRecommended from "eslint-plugin-prettier/recommended";
import simpleImportSort from "eslint-plugin-simple-import-sort";
import pluginRouter from "@tanstack/eslint-plugin-router";
import pluginQuery from "@tanstack/eslint-plugin-query";
import neverthrowMustUse from "eslint-plugin-neverthrow-must-use";
import tsParser from "@typescript-eslint/parser";

export default tseslint.config([
  globalIgnores([
    "dist",
    "node_modules",
    "build",
    "src/components/ui",
    "src/routeTree.gen.ts",
    "src/lib/i18n/resources.ts",
  ]),
  eslintPluginUnicorn.configs.recommended,
  eslintPluginPrettierRecommended,
  {
    files: ["**/*.{ts,tsx}"],
    extends: [
      js.configs.recommended,
      tseslint.configs.recommendedTypeChecked,
      eslintPluginReact.configs.flat.recommended,
      eslintPluginReact.configs.flat["jsx-runtime"],
      reactHooks.configs["recommended-latest"],
      reactRefresh.configs.vite,
    ],
    plugins: {
      "simple-import-sort": simpleImportSort,
      "@tanstack/react-router": pluginRouter,
      "@tanstack/query": pluginQuery,
      "neverthrow-must-use": neverthrowMustUse,
    },
    rules: {
      "no-unused-vars": "off",
      "@typescript-eslint/no-unused-vars": ["error", { argsIgnorePattern: "^_" }],
      eqeqeq: ["error", "always"],
      curly: ["error", "multi-or-nest"],
      "prefer-template": ["error"],
      "prefer-arrow-callback": ["error"],
      "object-shorthand": ["error", "always"],
      "arrow-body-style": ["error", "as-needed"],

      "unicorn/filename-case": ["error", { case: "kebabCase" }],
      "unicorn/prevent-abbreviations": [
        "error",
        {
          checkFilenames: false,
        },
      ],
      "unicorn/prefer-node-protocol": "off",

      "simple-import-sort/imports": "error",
      "simple-import-sort/exports": "error",

      "neverthrow-must-use/must-use-result": "error",
    },
    languageOptions: {
      ecmaVersion: 2020,
      globals: globals.browser,
      parser: tsParser,
      parserOptions: {
        project: ["./tsconfig.json", "./tsconfig.app.json", "./tsconfig.node.json"],
        projectService: true,
        tsconfigRootDir: import.meta.dirname,
      },
    },
    settings: {
      react: {
        version: "detect",
      },
    },
  },
]);
