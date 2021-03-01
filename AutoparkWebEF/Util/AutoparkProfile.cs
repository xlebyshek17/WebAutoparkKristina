using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.Util
{
    public class AutoparkProfile : Profile
    {
        public AutoparkProfile()
        {
            CreateMap<VehicleDto, VehicleViewModel>().ForMember(dest => dest.TypeName, act => act.MapFrom(src => src.Type.TypeName));
            CreateMap<VehicleTypeDto, VehicleTypeViewModel>();
            CreateMap<SparePartDto, SparePartViewModel>();
            CreateMap<OrderDto, OrderViewModel>().ForMember(dest => dest.VehicleName, act => act.MapFrom(src => src.Vehicle.ModelName));
            CreateMap<OrderItemDto, OrderItemViewModel>().ForMember(dest => dest.SparePartName, act => act.MapFrom(src => src.Detail.Name))
                .ForMember(dest => dest.VehicleName, act => act.MapFrom(src => src.Order.Vehicle.ModelName));

            CreateMap<VehicleViewModel, VehicleDto>();
            CreateMap<VehicleTypeViewModel, VehicleTypeDto>();
            CreateMap<SparePartViewModel, SparePartDto>();
            CreateMap<OrderViewModel, OrderDto>();
            CreateMap<OrderItemViewModel, OrderItemDto>();

            CreateMap<VehicleDto, Vehicle>();
            CreateMap<VehicleTypeDto, VehicleType>();
            CreateMap<SparePartDto, SparePart>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleType, VehicleTypeDto>();
            CreateMap<SparePart, SparePartDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
