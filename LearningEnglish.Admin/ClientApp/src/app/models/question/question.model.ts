export class Question {
    public id: string;
    public nameCourse: string;
    public name: string;
    public content: string;

    constructor(
        id?: string, nameCourse?: string, name?: string, content?: string,
    ) {
        this.id = id;
        this.nameCourse = nameCourse;
        this.name = name;
        this.content = content;
    }
}
