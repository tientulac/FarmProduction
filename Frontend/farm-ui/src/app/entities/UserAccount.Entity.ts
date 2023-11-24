import { BaseEntity } from "./Base.Entity";
import { RoleEntity } from "./Role.Entity";

export class UserAccountEntity extends BaseEntity {
    roleId!: string | null;
    userName!: string | null;
    hashPassword!: string | null;
    email!: string | null;
    phone!: string | null;
    fullName!: string | null;
    address!: string | null;
    status!: number | null;
    role!: RoleEntity;
    oldHashPassword!: string | null;
    newHashPassword!: string | null;
}

export class UserAccountEntitySearch extends BaseEntity {
    roleId!: string | null;
    userName!: string | null;
    hashPassword!: string | null;
    email!: string | null;
    phone!: string | null;
    fullName!: string | null;
    address!: string | null;
    status!: number | null;
    role!: RoleEntity;
}


export class UserAccountLogin extends BaseEntity {
    roleId!: string | null;
    userName!: string | null;
    hashPassword!: string | null;
    email!: string | null;
    phone!: string | null;
    fullName!: string | null;
    address!: string | null;
    status!: number | null;
    role!: RoleEntity;
    token!: string | null;
    oldHashPassword!: string | null;
    newHashPassword!: string | null;
}