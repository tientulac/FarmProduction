import { BaseEntity } from "./Base.Entity";

export class CategoryEntity extends BaseEntity {
    id!: number | null;
    code!: string | null;
    name!: string | null;
    image!: string | null;
}

export class CategoryEntitySearch extends BaseEntity {
    id!: number | null;
    code!: string;
    name!: string | null;
    image!: string | null;
}