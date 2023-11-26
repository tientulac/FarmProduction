import { BaseEntity } from "./Base.Entity";

export class ProductDescriptionEntity extends BaseEntity {
    productId!: string | null;
    description!: string | null;
}
