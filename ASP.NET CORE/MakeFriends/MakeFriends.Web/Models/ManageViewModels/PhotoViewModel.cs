using System.Collections.Generic;

namespace MakeFriends.Web.Models.ManageViewModels
{
    public class PhotoViewModel
    {
        public IEnumerable<string> PhotoPaths { get; set; }

        public string SelectedPhotoPath { get; set; }
    }
}
