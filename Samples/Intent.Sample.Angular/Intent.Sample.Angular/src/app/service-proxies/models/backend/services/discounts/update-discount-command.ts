export interface UpdateDiscountCommand {
  id: string;
  code: string;
  discountAmount: number;
  expiry: Date;
}
