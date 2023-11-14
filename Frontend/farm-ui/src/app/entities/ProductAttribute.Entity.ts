import { BaseEntity } from "./Base.Entity";

export class ProductAttributeEntity extends BaseEntity {
    productId!: string | null;
    code!: string | null;
    price!: number | null;
    amount!: number | null;
    image!: string | null;
    color!: string | null;
    unit!: string | null;
    value!: string | null;
    manufactureDate!: Date | null;
    expireDate!: Date | null;
}

export class ProductAttributeSearchEntity extends BaseEntity {
    productId!: string | null;
    code!: string | null;
    price!: number | null;
    amount!: number | null;
    image!: string | null;
    color!: string | null;
    unit!: string | null;
    value!: string | null;
    filterType!: FilterPriceType | null;
    manufactureDate!: Date | null;
    expireDate!: Date | null;
}

export enum FilterPriceType {
    GREATER,
    EQUAL,
    LESS
};