export class LessonForAdd {
    public id: string;
    public courseId: number;
    public name: string;
    public type: string;
    public introduce: string;
    public video: string;
    public tittle: string;

    constructor(
        id?: string, idCourse?: number, name?: string, type?: string,
        introduce?: string, video?: string, tittle?: string
    ) {
        this.id = id;
        this.courseId = idCourse;
        this.name = name;
        this.type = type;
        this.introduce = introduce;
        this.video = video;
        this.tittle = tittle;
    }
}
