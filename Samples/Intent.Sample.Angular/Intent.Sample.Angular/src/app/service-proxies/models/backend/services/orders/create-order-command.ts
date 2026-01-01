import { CreateOrderCommandOrderItemsDto } from './create-order-command-order-items-dto';


export interface CreateOrderCommand {
  customerId: string;
  orderNo: string;
  discountCode: string;
  total: number;
  orderedDate: Date;
  orderItems: CreateOrderCommandOrderItemsDto[];
}
