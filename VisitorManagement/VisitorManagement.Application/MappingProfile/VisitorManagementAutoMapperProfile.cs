using AutoMapper;
using VisitorManagement.Application.DTOs;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Application.MappingProfile
{
    public class VisitorManagementAutoMapperProfile : Profile
    {
        public VisitorManagementAutoMapperProfile()
        {
            CreateMap<Visitor, VisitorDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VisitorId))
                .ForMember(dest => dest.IsPreRegistered, opt => opt.MapFrom(src => src.PreRegisterVisitor))
                .ForMember(dest => dest.IsOnSiteRegistered, opt => opt.MapFrom(src => src.OnSiteRegisterVisitor))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Salutation))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.IdentificationType, opt => opt.MapFrom(src => src.IdType))
                .ForMember(dest => dest.IdentificationNumber, opt => opt.MapFrom(src => src.IdNumber))
                .ForMember(dest => dest.HostUserId, opt => opt.MapFrom(src => src.HostId))
                .ForMember(dest => dest.HostUserName, opt => opt.MapFrom(src => src.HostName))
                .ForMember(dest => dest.PurposeOfVisit, opt => opt.MapFrom(src => src.VisitPurpose))
                .ForMember(dest => dest.ExpectedArrival, opt => opt.MapFrom(src => src.ExpectedArrivalTime))
                .ForMember(dest => dest.ExpectedCheckOut, opt => opt.MapFrom(src => src.ExpectedCheckOutTime))
                .ForMember(dest => dest.VisitDuration, opt => opt.MapFrom(src => src.VisitDuration))
                .ReverseMap();
        }
    }
}