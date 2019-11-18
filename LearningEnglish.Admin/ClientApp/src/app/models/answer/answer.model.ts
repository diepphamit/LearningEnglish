export class Answer {
    public id: string;
    public questionName: string;
    public name: string;
    public content: string;
    public correctAnswer : boolean;

    constructor(
        id?: string, questionName?: string, name?: string, content?: string,
        correctAnswer?: boolean
    ) {
        this.id = id;
        this.questionName = questionName;
        this.name = name;
        this.content = content;
        this.correctAnswer = correctAnswer;
    }
}
