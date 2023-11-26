import { BaseEntity } from "./Base.Entity";
import { OrderEntity } from "./Order.Entity";
import { ProductAttributeEntity } from "./ProductAttribute.Entity";

export class OrderItemEntity extends BaseEntity {
    status!: number | null;
    orderId!: string | null;
    order!: OrderEntity | null;
    productAttributeId!: string | null;
    productAttribute!: ProductAttributeEntity | null;
    countBought!: number | null;
    unitPrice!: number | null;
}

export class OrderItemEntitySearch extends BaseEntity {
    status!: number | null;
    orderId!: string | null;
    order!: OrderEntity | null;
    productAttributeId!: string | null;
    productAttribute!: ProductAttributeEntity | null;
    unitPrice!: number | null;
}