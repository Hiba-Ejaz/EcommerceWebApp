export interface OrderRead {
    orderId: string;
    productId: string;
    productTitle: string;
    productPrice: number;
    quantity: number;
    totalAmount:number;
  };

export interface OrderProductsRead {
    productId: string;
    productTitle: string;
    productPrice: number;
    quantity: number;
}

export interface OrderWithDetailsRead {
    orderId: string;
    userId: string;
    email: string;
    name: string;
    orderItems: OrderProductsRead[];
    totalAmount: number;
}