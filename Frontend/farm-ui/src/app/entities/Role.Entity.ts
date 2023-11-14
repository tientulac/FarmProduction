import { BaseEntity } from "./Base.Entity";

export class RoleEntity extends BaseEntity {
    code!: string | null;
    name!: string | null;
    isActive!: boolean | null;
    isAdmin!: boolean | null;
}

export class RoleEntitySearch extends BaseEntity {
    code!: string | null;
    name!: string | null;
    isActive!: boolean | null;
    isAdmin!: boolean | null;
}