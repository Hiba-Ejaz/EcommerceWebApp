using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Implementations.Shared;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations
{
    public class OrderService : BaseService<Order, OrderCreateDto, OrderReadDto, OrderUpdateDto>, IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICartRepo _cartRepository;
        private readonly ICartItemsRepo _cartItemRepository;
        private readonly IOrderItemRepo _orderItemRepository;
        private readonly IUserRepo _userRepo;
        public OrderService(ICartRepo cartRepo, IOrderItemRepo orderItemRepository, IOrderRepo orderRepo, IUserRepo userRepo
        , IMapper mapper) : base(orderRepo,
         mapper)
        {
            _orderRepo = orderRepo;
            _orderItemRepository = orderItemRepository;
            _cartRepository = cartRepo;
            _userRepo = userRepo;
        }
        public async Task<string> AddOrder(Guid userIdGuid)
        {
            var cart = await _cartRepository.GetProcessingCartByUserId(userIdGuid) ?? throw CustomException.NotFoundException();
            var user = await _userRepo.GetOneById(userIdGuid) ?? throw CustomException.NotFoundException();
            var existingOrder = await _orderRepo.GetOneByUserId(userIdGuid);
            if (existingOrder == null)
            {
               var order = new Order
                {
                    User = user,
                    UserId = user.Id,
                    Status = OrderStatus.Processing,
                    TotalAmount = cart.TotalAmount,
                };
                var orderItems = new List<OrderItem>();
                foreach (var cartItem in cart.CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        Product = cartItem.Product,
                        Order = order,
                        Quantity = cartItem.Quantity,
                    };
                    orderItems.Add(orderItem);
                }
                order.OrderItems = orderItems;
                return await _orderRepo.AddOrder(order);
            }
            existingOrder.TotalAmount += cart.TotalAmount;
            var existingOrderItems = existingOrder.OrderItems;
            foreach (var cartItem in cart.CartItems)
            {
                var existingOrderItem = existingOrderItems.FirstOrDefault(oi => oi.Product.Id == cartItem.Product.Id);
                if (existingOrderItem != null)
                {
                    existingOrderItem.Quantity += cartItem.Quantity;
                }
                else
                {
                    var orderItem = new OrderItem
                    {
                        Product = cartItem.Product,
                        Order = existingOrder,
                        Quantity = cartItem.Quantity,
                    };
                    existingOrderItems.Add(orderItem);
                }
            }
            return await _orderRepo.UpdateOne(existingOrder);
        }
        public async Task<IEnumerable<OrderReadDto>> GetOrder(Guid userId)
        {
            var ordeerItems = await _orderItemRepository.GetOrderItems(userId)?? throw CustomException.NotFoundException();
            var orderItems = ordeerItems.Select(ci => new OrderReadDto
            {
                ProductId = ci.Product.Id,
                OrderId = ci.Order.Id,
                ProductTitle = ci.Product.Title,
                Quantity = ci.Quantity,
                ProductPrice = ci.Product.Price,
                TotalAmount = ci.Order.TotalAmount,
            }) ?? throw CustomException.NotFoundException();
            return orderItems;
        }
        public async Task<IEnumerable<OrderWithDetailsReadDto>> GetAllOrders()
        {
            var allOrders = await _orderRepo.GetAllOrders() ?? throw CustomException.NotFoundException();
            var orders = allOrders.Select(od => new OrderWithDetailsReadDto
            {
                UserId = od.UserId,
                OrderId = od.Id,
                Email = od.User.Email,
                TotalAmount = od.TotalAmount,
                Name = od.User.Name,
                orderItems = od.OrderItems.Select(oi => new OrderProductsReadDto
                {
                    ProductId = oi.Product.Id,
                    ProductTitle = oi.Product.Title,
                    ProductPrice = oi.Product.Price,
                    Quantity = oi.Product.Quantity,
                }).ToList()
            });
            return orders;
        }
    }
}