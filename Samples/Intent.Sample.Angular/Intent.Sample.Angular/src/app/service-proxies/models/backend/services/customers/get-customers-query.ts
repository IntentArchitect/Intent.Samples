export interface GetCustomersQuery {
  pageNo: number;
  pageSize: number;
  searchTerm: string | null;
  orderBy: string | null;
  isActive: boolean | null;
}
