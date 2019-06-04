using AutoMapper;
using AppModels = BT.Contacts.Application.Models;
using DomainModels = BT.Contacts.Domain.Entities;

namespace BT.Contacts.Application.Implementation.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppModels.Contact, DomainModels.Contact>().ReverseMap();
            CreateMap<AppModels.Address, DomainModels.Address>().ReverseMap();
        }
    }
}
