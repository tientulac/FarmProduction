import { BaseEntity } from "./Base.Entity";

export class BrandEntity extends BaseEntity {
    id!: number | null;
    code = Math.random().toString(36).substring(2, 7);
    name!: string | null;
    image!: string | null;
}

export class BrandEntitySearch extends BaseEntity {
    id!: number | null;
    code!: string;
    name!: string | null;
    image!: string | null;
}