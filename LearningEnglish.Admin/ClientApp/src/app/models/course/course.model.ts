export class Course {
    public id: string;
    public name: string;
    public introduce: string;
    public image: string;
    public levelClass: number;

  constructor(id?: string, name?: string, introduce?: string, image?: string, levelClass?: number) {
        this.id = id;
        this.name = name;
        this.introduce = introduce;
        this.image = image;
        this.levelClass = levelClass;
    }
}
