/**
 * PaginationResult interface defines the structure for paginated results.
 */
export interface PaginationResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
