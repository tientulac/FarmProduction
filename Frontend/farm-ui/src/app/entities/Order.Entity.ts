
import { BaseEntity } from "./Base.Entity";
import { UserAccountEntity } from "./UserAccount.Entity";

export class OrderEntity extends BaseEntity {
    code!: string | null;
    status!: number | null;
    type!: number | null;
    userAccountId!: string | null;
    userName!: string | null;
    total!: number | null;
    paymentShip!: number | null;
    paymentType!: number | null;
    wardFromId!: string | null;
    districtFromId!: string | null;
    provinceFromId!: string | null;
    wardToId!: string | null;
    districtToId!: string | null;
    provinceToId!: string | null;
    fromAddress!: string | null;
    toAddress!: string | null;
    fromDate!: Date | null;
    toDate!: Date | null;
    userAccount!: UserAccountEntity | null;
    address!: string | null;
}

export class OrderEntitySearch extends BaseEntity {
    code!: string | null;
    status!: number | null;
    type!: number | null;
    userAccountId!: string | null;
    total!: number | null;
    paymentShip!: number | null;
    paymentType!: number | null;
    wardFromId!: string | null;
    districtFromId!: string | null;
    provinceFromId!: string | null;
    wardToId!: string | null;
    districtToId!: string | null;
    provinceToId!: string | null;
    fromAddress!: string | null;
    tpAddress!: string | null;
    fromDate!: Date | null;
    toDate!: Date | null;
    userName!: string | null;
    userAccount!: UserAccountEntity | null;
    address!: string | null;
}
