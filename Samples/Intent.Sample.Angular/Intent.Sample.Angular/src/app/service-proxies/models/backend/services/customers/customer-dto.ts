import { CustomerTitleDto } from './customer-title-dto';
import { CustomerAddressDto } from './customer-address-dto';
import { CustomerLoyaltyDto } from './customer-loyalty-dto';


export interface CustomerDto {
  id: string;
  titleId: string;
  name: string;
  surname: string;
  email: string;
  isActive: boolean;
  title: CustomerTitleDto;
  addresses: CustomerAddressDto[];
  loyalty: CustomerLoyaltyDto | null;
}
