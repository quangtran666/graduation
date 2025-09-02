import * as React from "react";
import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
import { CheckIcon } from "lucide-react";
import { cva, type VariantProps } from "class-variance-authority";

import { cn } from "@/lib/utils";

const checkboxVariants = cva(
  "cursor-pointer peer border-input dark:bg-input/30 focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive shrink-0 rounded-[4px] border shadow-xs transition-shadow outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50",
  {
    variants: {
      variant: {
        default:
          "data-[state=checked]:bg-primary data-[state=checked]:text-primary-foreground dark:data-[state=checked]:bg-primary data-[state=checked]:border-primary",
        destructive:
          "data-[state=checked]:bg-destructive data-[state=checked]:text-destructive-foreground dark:data-[state=checked]:bg-destructive/80 data-[state=checked]:border-destructive focus-visible:ring-destructive/20 dark:focus-visible:ring-destructive/40",
        outline:
          "data-[state=checked]:bg-transparent data-[state=checked]:text-primary data-[state=checked]:border-primary",
      },
      size: {
        sm: "size-3",
        default: "size-4",
        lg: "size-5",
      },
    },
    defaultVariants: {
      variant: "default",
      size: "default",
    },
  },
);

type CheckboxVariantProps = VariantProps<typeof checkboxVariants>;

const checkboxIconSizes: Record<NonNullable<CheckboxVariantProps["size"]>, string> = {
  sm: "size-2.5",
  default: "size-3.5",
  lg: "size-4",
};

function Checkbox({
  className,
  variant,
  size,
  ...props
}: React.ComponentProps<typeof CheckboxPrimitive.Root> & CheckboxVariantProps) {
  const resolvedSize = (size ?? "default") as NonNullable<CheckboxVariantProps["size"]>;

  return (
    <CheckboxPrimitive.Root
      data-slot="checkbox"
      className={cn(checkboxVariants({ variant, size, className }))}
      {...props}
    >
      <CheckboxPrimitive.Indicator
        data-slot="checkbox-indicator"
        className="flex items-center justify-center text-current transition-none"
      >
        <CheckIcon className={checkboxIconSizes[resolvedSize]} />
      </CheckboxPrimitive.Indicator>
    </CheckboxPrimitive.Root>
  );
}

export { Checkbox, checkboxVariants };
