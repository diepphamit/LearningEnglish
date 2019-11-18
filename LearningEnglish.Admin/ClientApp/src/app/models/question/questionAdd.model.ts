export class QuestionForAdd {
    public id: string;
    public courseId: number;
    public name: string;
    public content: string;

    constructor(
        id?: string, courseId?: number, name?: string, content?: string
    ) {
        this.id = id;
        this.courseId = courseId;
        this.name = name;
        this.content = content;
    }
}
