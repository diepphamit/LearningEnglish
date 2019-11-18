export class UserCourse {
    public id: number;
    public userName: string;
    public courseName: string;
    constructor(id?: number, userName?: string, courseName?: string) {
        this.id = id;
        this.userName = userName;
        this.courseName = courseName;
    }
}