using AutoMapper;
using Services.B.Core.Dtos;
using Services.B.Core.Models;

namespace Services.B.Core.Profiles
{
    public class TemplateServiceProfiles : Profile
    {
        public TemplateServiceProfiles()
        {
            // source --> target
            //CreateMap<ServiceTemplateShortInfo, ServiceTemplateRead>();
            //CreateMap<ServiceTemplateFullInfo, ServiceTemplateByIdRead>();
            //CreateMap<ServiceTemplateCreate, ServiceTemplateFullInfo>();
        }
    }
}