export class Vocabulary {
    public id: string;
    public lessonName: string;
    public phonetic: string;
    public video: string;
    public audio: string;
    public name: string;

    constructor(
        id?: string, lessonName?: string, phonetic?: string, video?: string,
        audio?: string, name?: string
    ) {
        this.id = id;
        this.lessonName = lessonName;
        this.phonetic = phonetic;
        this.video = video;
        this.audio = audio;
        this.name = name;
    }
}
