import { UpdateCustomerCommandAddressesDto } from './update-customer-command-addresses-dto';
import { UpdateCustomerCommandLoyaltyDto } from './update-customer-command-loyalty-dto';


export interface UpdateCustomerCommand {
  id: string;
  titleId: string;
  name: string;
  surname: string;
  email: string;
  isActive: boolean;
  addresses: UpdateCustomerCommandAddressesDto[];
  loyalty: UpdateCustomerCommandLoyaltyDto | null;
}
