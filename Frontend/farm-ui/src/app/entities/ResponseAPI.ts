export class ReponseAPI<T> {
    Data!: T;
    StatusCode!: number | null;
    Message!: string | null;
    MessageEx!: string | null;
}