import { BaseEntity } from "./Base.Entity";
import { BrandEntity, BrandEntitySearch } from "./Brand.Entity";
import { CategoryEntity, CategoryEntitySearch } from "./Category.Entity";
import { ProducerEntity } from "./Producer.Entity";
import { ProductDescriptionEntity } from "./ProductDescription.Entity";
import { ProductImageEntity } from "./ProductImage.Entity";

export class ProductEntity extends BaseEntity {
    code!: string | null;
    name!: string | null;
    image!: string | null;
    brandId!: string | null;
    categoryId!: string | null;
    status!: string | null;
    category!: CategoryEntity;
    brand!: BrandEntity;
    producer!: ProducerEntity;
    description!: string | null;
    productDescriptions!: ProductDescriptionEntity[]; 
    productImages!: ProductImageEntity[];
}

export class ProductEntitySearch extends BaseEntity {
    code!: string | null;
    name!: string | null;
    image!: string | null;
    brandId!: string | null;
    categoryId!: string | null;
    status!: string | null;
    brandIds!: string[] | null;
    categoryIds!: string[] | null;
    category!: CategoryEntitySearch;
    brand!: BrandEntitySearch;
    producer!: ProducerEntity;
    description!: string | null;
    productDescriptions!: ProductDescriptionEntity[]; 
    productImages!: ProductImageEntity[];
}