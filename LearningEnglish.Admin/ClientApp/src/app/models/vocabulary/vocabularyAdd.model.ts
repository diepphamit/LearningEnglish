export class VocabularyForAdd {
    public id: string;
    public lessonId: number;
    public phonetic: string;
    public video: string;
    public audio: string;
    public name: string;

    constructor(
        id?: string, lessonId?: number, phonetic?: string, video?: string,
        audio?: string, name?: string
    ) {
        this.id = id;
        this.lessonId = lessonId;
        this.phonetic = phonetic;
        this.video = video;
        this.audio = audio;
        this.name = name;
    }
}
