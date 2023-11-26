import { BaseEntity } from "./Base.Entity";

export class BrandEntity extends BaseEntity {
    code = Math.random().toString(36).substring(2, 7);
    name!: string | null;
    image!: string | null;
}

export class BrandEntitySearch extends BaseEntity {
    code!: string;
    name!: string | null;
    image!: string | null;
}