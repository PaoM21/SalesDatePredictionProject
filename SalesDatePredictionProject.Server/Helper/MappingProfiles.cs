using AutoMapper;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employees, EmployeeDto>();
            CreateMap<Orders, OrderDto>();
            CreateMap<Products, ProductDto>();
            CreateMap<Shippers, ShipperDto>();
        }
    }
}
