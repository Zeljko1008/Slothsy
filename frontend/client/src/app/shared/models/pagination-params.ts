/**
 * Parameters for pagination and filtering in API requests.
 */
export interface PaginationParams {
  pageNumber?: number;
  pageSize?: number;
  includeInactive?: boolean;
}
