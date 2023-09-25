export class BaseEntity {
    CreatedAt!: Date | null;
    CreatedById!: any | null;
    UpdatedAt!: Date | null;
    UpdatedById!: any | null;
    DeletedAt!: Date | null;
    DeletedById!: any | null;
    IsSoftDeleted!: any | null;
}