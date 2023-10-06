export class ReponseAPI<T> {
    data!: T;
    code!: number | null;
    message!: string | null;
    messageEX!: string | null;
}