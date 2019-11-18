export class VocabularyForAdd {
    public id: string;
    public lessonId: number;
    public phonetic: string;
    public video: string;
    public audio: string;

    constructor(
        id?: string, lessonId?: number, phonetic?: string, video?: string,
        audio?: string
    ) {
        this.id = id;
        this.lessonId = lessonId;
        this.phonetic = phonetic;
        this.video = video;
        this.audio = audio;
    }
}
