using AutoMapper;
using Payments.Entities.DTOs;
using Payments.Entities.Entities;

namespace Payments.Core.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingDto, Booking>();
            CreateMap<Booking, BookingDto>();
        }
    }
}
