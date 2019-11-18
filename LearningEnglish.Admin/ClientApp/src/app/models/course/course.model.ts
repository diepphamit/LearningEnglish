export class Course {
    public id: string;
    public name: string;
    public introduce: string;
    public image: string;

  constructor(id?: string, name?: string, introduce?: string, image?: string) {
        this.id = id;
        this.name = name;
        this.introduce = introduce;
        this.image = image;
    }
}
