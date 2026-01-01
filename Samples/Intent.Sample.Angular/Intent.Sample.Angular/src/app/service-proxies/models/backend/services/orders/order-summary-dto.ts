export interface OrderSummaryDto {
  id: string;
  customerId: string;
  orderNo: string;
  total: number;
  orderedDate: Date;
  customerName: string;
  customerSurname: string;
  customerEmail: string;
  titleName: string;
}
