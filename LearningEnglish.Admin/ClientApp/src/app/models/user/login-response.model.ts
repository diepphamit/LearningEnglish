export interface LoginResponse {
    access_token: string;
    user: UserResponse;
}


export interface UserResponse {
    id: string;
    email: string;
    userName: string;
    fullName: string;
}

export interface Token {
    nameid: string;
    unique_name: string;
    role: string | string[];
    configuration: string;
}
