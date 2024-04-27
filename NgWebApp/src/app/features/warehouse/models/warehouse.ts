export interface Warehouse {
  id: string;
  name: string;
  addressId: string;
  isEnabled: boolean;
  createdAt: string;
  updatedAt: string;
}

export type AddWarehouse = Pick<Warehouse, 'name' | 'addressId'>;
