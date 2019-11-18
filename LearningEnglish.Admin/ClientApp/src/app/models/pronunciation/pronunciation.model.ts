export class Pronunciation {
    public id: string;
    public lessonName: string;
    public phonetic: string;
    public video: string;
    public audio: string;

    constructor(
        id?: string, lessonName?: string, phonetic?: string, video?: string,
        audio?: string
    ) {
        this.id = id;
        this.lessonName = lessonName;
        this.phonetic = phonetic;
        this.video = video;
        this.audio = audio;
    }
}
