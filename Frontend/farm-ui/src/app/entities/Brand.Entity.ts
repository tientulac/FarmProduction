import { BaseEntity } from "./Base.Entity";

export class BrandEntity extends BaseEntity {
    id!: number | null;
    code!: string | null;
    name!: string | null;
}