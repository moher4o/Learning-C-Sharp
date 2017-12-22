using AutoMapper.QueryableExtensions;
using MakeFriends.Data;
using MakeFriends.Data.Models;
using MakeFriends.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MakeFriends.Data.DataConstants;

namespace MakeFriends.Services.Implementations
{
   public class MessageService : IMessageService
    {
        private readonly FriendsDbContext db;

        public MessageService(FriendsDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddNewMessage(string senderId, string receiverId, string content)
        {
            if (senderId == null || receiverId == null)
            {
                return false;
            }

            if (content.Length < MessageMinLength || content.Length > MessageMaxLength)
            {
                return false;
            }


            var senderUser = this.db.Users.FirstOrDefault(u => u.Id == senderId);
            var receiverUser = this.db.Users.FirstOrDefault(u => u.Id == receiverId);

            if (senderUser == null || receiverUser == null)
            {
                return false;
            }

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
            };

            await this.db.Messages.AddAsync(message);
            await this.db.SaveChangesAsync();

            return true;
        }

        public IEnumerable<MessagesServiceModel> GetMessages(string firstUserId, string secondUserId)
        {
            if (firstUserId == null || secondUserId == null)
            {
                return null;
            }

            var conversation = this.db.Messages
                .Where(m => (m.SenderId == firstUserId && m.ReceiverId == secondUserId) || (m.SenderId == secondUserId && m.ReceiverId == firstUserId))
                .ProjectTo<MessagesServiceModel>()
                .ToList();

            return conversation;
        }
    }
}
