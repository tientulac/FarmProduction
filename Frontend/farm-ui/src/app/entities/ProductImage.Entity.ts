import { BaseEntity } from "./Base.Entity";

export class ProductImageEntity extends BaseEntity {
    productId!: string | null;
    image!: string | null;
    images!: string[] | null;
}
