import { BaseEntity } from "./Base.Entity";

export class CategoryEntity extends BaseEntity {
    code!: string | null;
    name!: string | null;
    image!: string | null;
    subCategories!: SubCategoryEntity[] | null;
}

export class SubCategoryEntity extends BaseEntity {
    code!: string | null;
    name!: string | null;
    image!: string | null;
    parentCategoryId!: string | null;
}

export class CategoryEntitySearch extends BaseEntity {
    code!: string;
    name!: string | null;
    image!: string | null;
    parentCategoryId!: string | null;
    subCategories!: SubCategoryEntity[] | null;
}