using AutoMapper;
using Perguntech.API.DTO;
using Perguntech.Core.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<QuestionDto, QuestionDomain>();
        CreateMap<QuestionDomain, QuestionDto>();
    }
}
