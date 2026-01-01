import { OrderOrderItemDto } from './order-order-item-dto';


export interface OrderDto {
  id: string;
  customerId: string;
  orderNo: string;
  discountCode: string;
  total: number;
  orderedDate: Date;
  customerName: string;
  customerSurname: string;
  customerEmail: string;
  titleName: string;
  orderItems: OrderOrderItemDto[];
}
