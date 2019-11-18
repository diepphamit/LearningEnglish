export class Achievement {
    public id: number;
    public point: number;
    public testDate: Date;
    public courseName: string;
    public userName: string;
    constructor(id?: number, point?: number, testDate?: Date,
        courseName?: string, userName?: string
    ) {
        this.id = id;
        this.point = point;
        this.testDate = testDate;
        this.courseName = courseName;
        this.userName = userName;
    }
}