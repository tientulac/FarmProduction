export class ReponseAPI<T> {
    data!: T;
    code!: string | null;
    message!: string | null;
    messageEX!: string | null;
}