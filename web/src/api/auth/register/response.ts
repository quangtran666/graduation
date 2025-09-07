export interface RegisterResponse {
  message: string;
  user: {
    id: number;
    username: string;
    email: string;
    emailVerified: boolean;
  };
}
