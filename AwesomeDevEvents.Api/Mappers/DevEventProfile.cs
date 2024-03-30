using AutoMapper;
using AwesomeDevEvents.Api.Entities;
using AwesomeDevEvents.Api.Models;

namespace AwesomeDevEvents.Api.Mappers;

public class DevEventProfile : Profile
{
    public DevEventProfile()
    {
        CreateMap<DevEvent, DevEventViewModel>();
        CreateMap<DevEventSpeaker, DevEventSpeakerViewModel>();

        CreateMap<DevEventInputModel, DevEvent>();
        CreateMap<DevEventSpeakerInputModel, DevEventSpeaker>();
    }
}
