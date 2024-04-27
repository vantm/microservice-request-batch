export interface Batch {
  id: string;
  key: string;
  productId: string;
  warehouseId: string;
  quantity: number;
  createdAt: string;
  updatedAt: string;
}

export type AddBatch = Pick<
  Batch,
  'key' | 'productId' | 'warehouseId' | 'quantity'
>;
