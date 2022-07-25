using AutoMapper;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notification, NotificationResponse>();
        }
    }
}
