import { AddressType } from './../../../address-type';


export interface UpdateCustomerCommandAddressesDto {
  id: string;
  line1: string;
  line2: string;
  city: string;
  postal: string;
  addressType: AddressType;
}
