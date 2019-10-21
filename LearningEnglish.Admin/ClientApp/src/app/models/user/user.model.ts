export class User {
    public id: string;
    public userName: string;
    public fullName: string;
    public email: string;
    public roles: string[];

    constructor(id?: string, username?: string, fullname?: string, email?: string, roles?: string[]) {
        this.id = id;
        this.userName = username;
        this.fullName = fullname;
        this.email = email;
        this.roles = roles;
    }
}
