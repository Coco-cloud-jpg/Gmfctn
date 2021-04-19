export enum Roles {
    User = 'User',
    Admin = 'Admin'
}

export interface User {
    token?: string;
    refreshToken?: string;
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    xp: number;
    status?: string;
    roles: Roles[];
    badges?: number;
    avatarId: string;
  }
