import { CreateCustomerCommandAddressesDto } from './create-customer-command-addresses-dto';
import { CreateCustomerCommandLoyaltyDto } from './create-customer-command-loyalty-dto';


export interface CreateCustomerCommand {
  titleId: string;
  name: string;
  surname: string;
  email: string;
  isActive: boolean;
  addresses: CreateCustomerCommandAddressesDto[];
  loyalty: CreateCustomerCommandLoyaltyDto | null;
}
