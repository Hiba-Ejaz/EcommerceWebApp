using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Implementations.Shared;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepository;
        private readonly ICartItemsRepo _cartItemRepository;
        private readonly IProductRepo _productRepository;
        private readonly IUserRepo _userRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepo cartRepository, ICartItemsRepo cartItemRepository, IUserRepo userRepository, IProductRepo productRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;

        }
        // public async Task<IEnumerable<CartReadDto>> GetCartItems(Guid userId)
        // {
        //     var carrtItems = await _cartItemRepository.GetCartItems(userId) ?? throw CustomException.NotFoundException();
        //     var cartItems = carrtItems.Select(ci => new CartReadDto
        //     {
        //         ProductId = ci.Product.Id,
        //         ProductTitle = ci.Product.Title,
        //         Quantity = ci.Quantity,
        //         ProductPrice = ci.Product.Price,
        //         TotalAmount = ci.Cart.TotalAmount,
        //     });
        //     return cartItems;
        // }
        public async Task<IEnumerable<CartReadDto>> GetCartItems(Guid userId)
        {
            try
            {
                var carrtItems = await _cartItemRepository.GetCartItems(userId) ?? throw CustomException.NotFoundException();
                var cartItems = carrtItems.Select(ci => new CartReadDto
                {
                    ProductId = ci.Product.Id,
                    ProductTitle = ci.Product.Title,
                    Quantity = ci.Quantity,
                    ProductPrice = ci.Product.Price,
                    TotalAmount = ci.Cart.TotalAmount,
                }) ?? throw CustomException.NotFoundException();
                return cartItems;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching cart items.", ex);
            }

        }

        public async Task<string> AddToCart(Guid userId, AddToCartDto addToCartDto)
        {
            var product = await _productRepository.GetOneById(addToCartDto.ProductId);
            var user = await _userRepository.GetOneById(userId);
            if (product == null || product.Quantity < addToCartDto.Quantity)
            {
                throw CustomException.NotFoundException();
            }
            if (user == null)
            {
                throw CustomException.NotFoundException();
            }
            var existingCart = await _cartRepository.GetProcessingCartByUserId(userId);
            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    UserId = userId,
                    Status = OrderStatus.Processing,
                    CartItems = new List<CartItem>()
                };
                var createdCart = await _cartRepository.CreateOne(newCart);
                createdCart.CartItems = new List<CartItem>();
                var cartItem = new CartItem
                {
                    Cart = createdCart,
                    Product = product,
                    Quantity = addToCartDto.Quantity,

                };
                newCart.CartItems.Add(cartItem);
                newCart.TotalAmount = cartItem.Product.Price;
                product.Quantity-=1;
                await _productRepository.UpdateOne(product.Id,product);
                return await _cartRepository.UpdateCart(newCart);
            }
            else if (existingCart != null)
            {
                try
                {
                    var existingCartItem = await _cartItemRepository.GetCartItem(existingCart.Id, addToCartDto.ProductId);
                    if (existingCartItem != null)
                    {
                        if (addToCartDto.Quantity < 0)
                        {
                            var producttoupdatequantity1 = await _productRepository.GetOneById(addToCartDto.ProductId);
                            if(producttoupdatequantity1 != null){
                            producttoupdatequantity1.Quantity += 1;    
                            await _productRepository.UpdateOne(producttoupdatequantity1.Id,producttoupdatequantity1);
                            }
                            if (existingCartItem.Quantity == 0)
                            {
                                await _cartItemRepository.RemoveFromCart(userId, addToCartDto.ProductId);
                            }
                            if (existingCartItem.Quantity > 0)
                            {
                                existingCart.TotalAmount -= existingCartItem.Product.Price;
                            }
                        }
                        else
                        {
                            var producttoupdatequantity2 = await _productRepository.GetOneById(addToCartDto.ProductId);
                            if(producttoupdatequantity2 != null){
                            producttoupdatequantity2.Quantity -= 1;    
                            await _productRepository.UpdateOne(producttoupdatequantity2.Id,producttoupdatequantity2);
                            }
                            existingCartItem.Quantity += 1;
                            existingCart.TotalAmount += existingCartItem.Product.Price;
                        }
                        return await _cartItemRepository.UpdateCartItem(existingCartItem);
                    }

                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error updating order: {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        errorMessage += $" (Inner Exception: {ex.InnerException.Message})";
                    }
                }
                var cartItem2 = new CartItem
                {
                    Cart = existingCart,
                    Product = product,
                    Quantity = addToCartDto.Quantity,
                };
                existingCart.TotalAmount+=cartItem2.Product.Price;
                 var producttoupdatequantity = await _productRepository.GetOneById(cartItem2.Product.Id);
                            if(producttoupdatequantity != null){
                            producttoupdatequantity.Quantity -= 1;    
                            await _productRepository.UpdateOne(producttoupdatequantity.Id,producttoupdatequantity);
                            }
                existingCart.CartItems.Add(cartItem2);
                return await _cartRepository.UpdateCart(existingCart);
            }
            return "";
        }
        public async Task<string> RemoveFromCart(Guid userIdGuid, Guid productId)
        {
            try
            {
                return await _cartItemRepository.RemoveFromCart(userIdGuid, productId);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, $"Error deleting cart: {ex.Message}");
            }
        }
        public async Task<string> DeleteCart(Guid userIdGuid)
        {
            try
            {
                var cart = await _cartRepository.GetProcessingCartByUserId(userIdGuid);

                if (cart != null)
                {
                    foreach (var cartItem in cart.CartItems.ToList()) // ToList creates a snapshot
                    {
                        await _cartItemRepository.DeleteOneById(cartItem);
                    }

                    await _cartRepository.DeleteOneById(cart);

                    return "Cart and associated items deleted successfully";
                }
                else
                {
                    throw CustomException.NotFoundException();
                }
            }
            catch (Exception ex)
            {
                return $"Error deleting cart: {ex.Message}";
            }
        }
    }
}

