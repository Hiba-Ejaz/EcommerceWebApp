

using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;


namespace WebApi.Business.src.Implementations
{
    public class CartService: ICartService
    {
        private readonly IOrderRepo _orderRepository;
        private readonly IProductRepo _productRepository;
        private readonly IUserRepo _userRepository;
        private readonly IMapper _mapper;
   
            public CartService(IOrderRepo orderRepository,IUserRepo userRepository, IProductRepo productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;   
            _mapper = mapper;
            
        }
        public async Task<string> AddToCart(Guid userId,AddToCartDto addToCartDto)
        {
         var product = await _productRepository.GetOneById(addToCartDto.ProductId);
         var user=await _userRepository.GetOneById(userId);
        if (product == null || product.Quantity < addToCartDto.Quantity)
        {
            return "false; // Product not found or insufficient quantity";
        }
          if (product == null )
        {
            return "false; // User not found ";
        }
     var existingOrder = await _orderRepository.GetProcessingOrderByUserId(userId);
        if (existingOrder == null)
    {
        existingOrder = await _orderRepository.GetOrCreateCartForUser(userId);
    }
       
        //var order = await _orderRepository.GetOrCreateCartForUser(userId);

         var productPresent = existingOrder.OrderItems.FirstOrDefault(oi =>
        oi.Product.Id == product.Id);

        if (productPresent != null)
        {
        productPresent.Quantity+=addToCartDto.Quantity;
        }
        else
        {
            var orderItem = new OrderItem
            { 
                Product = product,
                Quantity = addToCartDto.Quantity
            };
            existingOrder.OrderItems.Add(orderItem);
        }

       
        return await _orderRepository.AddOrder(existingOrder);
        }}
}