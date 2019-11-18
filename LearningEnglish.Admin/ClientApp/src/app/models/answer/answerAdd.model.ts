export class AnswerAdd {
    public id: string;
    public questionId: number;
    public name: string;
    public content: string;
    public correctAnswer : boolean;

    constructor(
        id?: string, questionId?: number, name?: string, content?: string,
        correctAnswer?: boolean
    ) {
        this.id = id;
        this.questionId = questionId;
        this.name = name;
        this.content = content;
        this.correctAnswer = correctAnswer;
    }
}
