import { AddressType } from './../../../address-type';


export interface CreateCustomerCommandAddressesDto {
  line1: string;
  line2: string;
  city: string;
  postal: string;
  addressType: AddressType;
}
