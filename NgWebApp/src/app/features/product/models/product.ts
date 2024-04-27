export interface Product {
  id: string;
  name: string;
  isEnabled: boolean;
  price: number;
  createdAt: string;
  updatedAt: string;
}

export type AddProduct = Pick<Product, 'name' | 'price'>;
