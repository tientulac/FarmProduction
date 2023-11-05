import { BaseEntity } from "./Base.Entity";

export class ProductDescriptionEntity extends BaseEntity {
    id!: number | null;
    productId!: string | null;
    description!: string | null;
}
