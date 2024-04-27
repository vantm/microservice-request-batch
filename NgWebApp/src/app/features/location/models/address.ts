export interface Address {
  id: string;
  addressText: string;
  district: string;
  city: string;
  createdAt: string;
  updatedAt: string;
}

export type AddAddress = Omit<Address, 'id' | 'createdAt' | 'updatedAt'>;
