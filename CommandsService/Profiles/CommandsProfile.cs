using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Profiles
{
    public class CommandsProfile: Profile
    {
        public CommandsProfile()
        {
            //Source->Target 
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreatDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest=>dest.ExternalID,opt=>opt.MapFrom(src=>src.Id));
        }
    }
}