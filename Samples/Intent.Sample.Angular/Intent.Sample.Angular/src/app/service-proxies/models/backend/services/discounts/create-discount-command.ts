export interface CreateDiscountCommand {
  code: string;
  discountAmount: number;
  expiry: Date;
}
