import { BaseEntity } from "./Base.Entity";

export class BrandEntity extends BaseEntity {
    id!: number | null;
    code!: string | null;
    name!: string | null;
    image!: string | null;
}

export class BrandEntitySearch extends BaseEntity {
    id!: number | null;
    code!: string;
    name!: string | null;
    image!: string | null;
}