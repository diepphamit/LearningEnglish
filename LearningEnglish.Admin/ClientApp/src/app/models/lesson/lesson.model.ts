export class Lesson {
    public id: string;
    public nameCourse: string;
    public name: string;
    public type: string;
    public introduce: string;
    public video: string;
    public tittle: string;

    constructor(
        id?: string, nameCourse?: string, name?: string, type?: string,
        introduce?: string, video?: string, tittle?: string
    ) {
        this.id = id;
        this.nameCourse = nameCourse;
        this.name = name;
        this.type = type;
        this.introduce = introduce;
        this.video = video;
        this.tittle = tittle;
    }
}
