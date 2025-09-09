interface Resources {
  "auth": {
    "email": {
      "resetPassword": {
        "button": "Reset password",
        "contactSupport": "If you didn’t request a password reset, you can safely ignore this email. For help, contact us at {{supportEmail}}.",
        "description": "Tap the button below to create a new password for {{appName}}.",
        "linkExpired": "This link may expire after a short period for security. If the button above doesn’t work, copy and paste the URL below into your browser:",
        "preview": "Reset your password for {{appName}}",
        "title": "Reset your password"
      },
      "verification": {
        "contactSupport": "If you didn’t request this, you can safely ignore this email. For help, contact us at {{supportEmail}}.",
        "description": "Tap the button below to confirm this email address for {{appName}}.",
        "linkExpired": "This link may expire after a short period for security. If the button above doesn’t work, copy and paste the URL below into your browser:",
        "preview": "Verify your email for {{appName}}",
        "title": "Verify your email",
        "verifyEmail": "Verify email"
      }
    }
  },
  "form": {
    "common": {
      "backTo": "Back to {{page}}",
      "continueWith": "Continue with {{action}}",
      "form": {
        "emailNoAt": "{{field}} cannot be an email address",
        "max": "The field must be at most {{length}} characters long",
        "min": "The field must be at least {{length}} characters long",
        "name": {
          "confirmPassword": "Confirm password",
          "email": "Email",
          "password": "Password",
          "username": "Username"
        },
        "required": "This {{field}} is required"
      },
      "gotohome": "Go to homepage",
      "orContinueWith": "Or continue with",
      "signIn": "Sign in",
      "signInWith": "Login with {{provider}}",
      "signUpWith": "Sign up with {{provider}}"
    },
    "emailConfirm": {
      "description": "Your email has been successfully verified. You can now access all features of your account.",
      "error": {
        "description": "We couldn't verify your email. The link may be expired or invalid.",
        "failed": "Email verification failed. The verification link may be invalid or expired.",
        "title": "Verification Failed"
      },
      "success": {
        "description": "Your email has been verified. You can now sign in to your account.",
        "title": "Email Verified Successfully!"
      },
      "title": "Email verified",
      "verifying": {
        "description": "Please wait while we verify your email address...",
        "title": "Verifying Your Email"
      }
    },
    "forgotPassword": {
      "description": "Enter your email below to reset your password",
      "email": "Email",
      "rememberPassword": "Remember your password?",
      "resetPassword": "Reset password",
      "success": "Password reset email sent successfully. Please check your email.",
      "title": "Forgot your password?",
      "validation": {
        "emailInvalid": "Email is invalid"
      }
    },
    "login": {
      "description": "Enter your email below to login to your account",
      "email": "Email",
      "error": "Login failed",
      "forgotPassword": "Forgot your password?",
      "noAccount": "Don't have an account?",
      "pageTitle": "Login",
      "password": "Password",
      "remember": "Remember me",
      "signUp": "Sign up",
      "success": "Login successful",
      "title": "Login to your account",
      "usernameOrEmail": "Username or email",
      "validation": {
        "passwordRequired": "Password is required",
        "usernameOrEmailInvalid": "Username or email is invalid"
      }
    },
    "register": {
      "confirmPassword": "Confirm password",
      "description": "Enter your email below to create an account",
      "email": "Email",
      "error": {
        "failed": "Registration failed. Please try again.",
        "invalidInput": "Please check your input",
        "userExists": "User already exists"
      },
      "pageTitle": "Register",
      "password": "Password",
      "signUp": "Sign up",
      "success": "Account created successfully, please check your email to verify",
      "title": "Create an account",
      "username": "Username",
      "validation": {
        "confirmPassword": "Confirm password is required",
        "email": "Email is required",
        "password": "Password is required",
        "passwordMismatch": "Passwords do not match",
        "username": "Username is required",
        "usernameNoSpaces": "Username cannot contain spaces"
      }
    },
    "resetPassword": {
      "confirmPassword": "Confirm password",
      "description": "Enter your new password below",
      "error": "Password reset failed",
      "password": "New password",
      "rememberPassword": "Remember your password?",
      "resetPassword": "Reset password",
      "success": "Password reset successfully! Please sign in with your new password.",
      "title": "Reset password",
      "validation": {
        "passwordMismatch": "Passwords do not match",
        "tokenInvalid": "Invalid or missing reset token"
      }
    },
    "verifyEmail": {
      "backToLogin": "Back to login",
      "cooldown": "Retry in {{seconds}}s.",
      "description": "A verification link has been sent to your email. Please check your inbox and click the link to verify your email address.",
      "error": {
        "failed": "Failed to resend verification email. Please try again later.",
        "userNotFound": "User not found",
        "waitingPeriodOrAlreadyVerified": "Email is already verified or waiting for cooldown period"
      },
      "resend": "Resend verification email",
      "resendTitle": "Didn't receive the email? Check your spam folder or try resending.",
      "success": "Verification email resent successfully. Please check your email.",
      "title": "Verify your email"
    }
  },
  "metadata": {
    "emailConfirmed": {
      "title": "Email Confirmed"
    },
    "forgotPassword": {
      "title": "Forgot Password"
    },
    "login": {
      "title": "Login"
    },
    "register": {
      "title": "Register"
    },
    "resetPassword": {
      "title": "Reset Password"
    },
    "verifyEmail": {
      "title": "Verify Email"
    }
  }
}

export default Resources;
