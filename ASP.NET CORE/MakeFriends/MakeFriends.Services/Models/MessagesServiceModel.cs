using AutoMapper;
using MakeFriends.Common.Mapping;
using MakeFriends.Data.Models;

namespace MakeFriends.Services.Models
{
    public class MessagesServiceModel : IMapFrom<Message>, IHaveCustomMapping
    {
        public string SenderId { get; set; }

        public string SenderFirstName { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverFirstName {get; set;}

        public string Content { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Message, MessagesServiceModel>()
                   .ForMember(u => u.SenderFirstName, cfg => cfg.MapFrom(u => u.Sender.FirstName));
            profile.CreateMap<Message, MessagesServiceModel>()
                   .ForMember(u => u.ReceiverFirstName, cfg => cfg.MapFrom(u => u.Receiver.FirstName));

        }
    }
}
