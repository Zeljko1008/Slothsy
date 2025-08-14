export interface LoginResponse {
   success: boolean;
  title: string;
  message: string;
  userId?: string;
  firstName?: string;
  accessToken: string;
  refreshToken: string;
  roles?: string[];
}
