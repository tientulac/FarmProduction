import { BaseEntity } from "./Base.Entity";

export class BrandEntity extends BaseEntity {
    Id!: number | null;
    Code!: string | null;
    Name!: string | null;
}