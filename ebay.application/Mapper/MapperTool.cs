using System.Text.Json;
using AutoMapper;
using ebay.domain.Entities;


public class MapperTool : Profile
{
    public MapperTool()
    {
        CreateMap<ProductEntity, GetProductDto>().ReverseMap();
    }
}