using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MakeFriends.Services
{
   public interface IUploadFile
    {
        bool ProcessPhoto(Stream inputStream, string userId, string photoName);

        bool DeleteUserPhoto(string photopath);

        bool DeleteUserDirectory(string userId);
    }
}
