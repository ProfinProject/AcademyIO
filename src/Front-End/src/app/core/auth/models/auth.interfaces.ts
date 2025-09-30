export interface LoginCredentials {
  email: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  expiresIn: number;
  userToken: UserToken;
}

export interface UserToken {
  id: string;
  email: string;
  claims: Claim[];
}

export interface Claim {
  value: string;
  type: string;
}

export interface RegisterCredentials {
  email: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string; // O formato deve ser 'YYYY-MM-DD'
  password: string;
  confirmPassword?: string;
  isAdmin?: boolean;
}
