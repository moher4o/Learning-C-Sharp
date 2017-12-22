using MakeFriends.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriends.Services
{
    public interface IUserService
    {
        Task<IQueryable<FullUserInfoServiceModel>> GetUserInfoAsync(string userId, string observerId);
    }
}
