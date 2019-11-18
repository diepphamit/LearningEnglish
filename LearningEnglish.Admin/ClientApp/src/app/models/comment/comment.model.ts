export class Comment {
    public id: number;
    public userName: string;
    public content: string;

    constructor(id?: number, userName?: string, content?: string) {
        this.id = id;
        this.userName = userName;
        this.content = content;
    }
}