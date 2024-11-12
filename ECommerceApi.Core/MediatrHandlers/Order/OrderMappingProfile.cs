using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.Order.Commands;
using ECommerceApi.Data.Entities;

namespace ECommerceApi.Core.MediatrHandlers.Order;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Data.Entities.Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<CreateOrderCommand, Data.Entities.Order>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateOrderCommand, Data.Entities.Order>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<CreateOrderItemCommand, OrderItem>()
            .ForMember(dest => dest.OrderItemId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<AddOrderItemCommand,OrderItem>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateOrderItemCommand, OrderItem>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}