export class User {
    public id: string;
    public userName: string;
    public fullName: string;
    public email: string;
    public roles: string[];
    public gender: boolean;
    public dateOfBirth: Date;

    constructor(id?: string, username?: string, fullname?: string, email?: string, roles?: string[]) {
        this.id = id;
        this.userName = username;
        this.fullName = fullname;
        this.email = email;
        this.roles = roles;
    }
}

export class UserForList {
    public id: string;
    public userName: string;
    public fullName: string;
    public email: string;
    public role: string;

    constructor(id?: string, username?: string, fullname?: string, email?: string, role?: string) {
        this.id = id;
        this.userName = username;
        this.fullName = fullname;
        this.email = email;
        this.role = role;
    }
}

