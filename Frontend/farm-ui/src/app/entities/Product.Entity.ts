import { BaseEntity } from "./Base.Entity";

export class ProductEntity extends BaseEntity {
    id!: number | null;
    code!: string | null;
    name!: string | null;
    image!: string | null;
    brandId!: string | null;
    categoryId!: string | null;
    status!: string | null;
}

export class ProductEntitySearch extends BaseEntity {
    id!: number | null;
    code!: string | null;
    name!: string | null;
    image!: string | null;
    brandId!: string | null;
    categoryId!: string | null;
    status!: string | null;
    brandIds!: string[] | null;
    categoryIds!: string[] | null;
}