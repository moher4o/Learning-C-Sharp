using MakeFriends.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakeFriends.Services
{
   public interface IMessageService
    {
        Task<bool> AddNewMessage(string senderId, string receiverId, string content);

        IEnumerable<MessagesServiceModel> GetMessages(string firstUserId, string secondUserId);
    }
}
