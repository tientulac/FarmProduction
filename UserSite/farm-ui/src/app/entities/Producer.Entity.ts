import { BaseEntity } from "./Base.Entity";

export class ProducerEntity extends BaseEntity {
    code!: string | null;
    name!: string | null;
    address!: string | null;
    phoneNumber!: string | null;
    email!: string | null;
    website!: string | null;
    image!: string | null;
    TypeModel!: number | null;
    mainMarketing!: string | null;
    description!: string | null;
    yearBorn!: number | null;
}

export class ProducerEntitySearch extends BaseEntity {
    code!: string | null;
    name!: string | null;
    address!: string | null;
    phoneNumber!: string | null;
    email!: string | null;
    website!: string | null;
    image!: string | null;
    TypeModel!: number | null;
    mainMarketing!: string | null;
    description!: string | null;
    yearBorn!: number | null;
}