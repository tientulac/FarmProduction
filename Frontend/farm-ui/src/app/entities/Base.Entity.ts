export class BaseEntity {
    createdAt!: Date | null;
    createdById!: any | null;
    updatedAt!: Date | null;
    updatedById!: any | null;
    deletedAt!: Date | null;
    deletedById!: any | null;
    isSoftDeleted!: any | null;
}