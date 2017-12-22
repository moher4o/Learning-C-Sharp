namespace MakeFriends.Data
{
    public static class DataConstants
    {
        public const int PictureNameMinLength = 1;
        public const int PictureNameMaxLength = 8;
        public const int UserPhotoLimit = 30;
        public const string UserPhotoSubDirectory = "UserPhotos";
        public const string FirstUserPhotoName = "1.jpg";


        public const int MessageMinLength = 1;
        public const int MessageMaxLength = 200;

        public const int DescriptionMinLength = 1;
        public const int DescriptionMaxLength = 300;

        public const int UserNameMinLength = 2;
        public const int UserNameMaxLength = 22;

        public const string AdministratorRole = "Administrator";

        public const string SuccessMessageKey = "SuccessMessage";
        public const string ErrorMessageKey = "ErrorMessage";

        //Admin Data
        public const string AdminUsername = "admin@abv.bg";
        public const string AdminFirstName = "Angel";
        public const string AdminLastName = "Vukov";
        public const string AdminPassword = "admin12";
        public const string AdminBirthDate = "02-01-1973";

        public const int AdminUsersPerPageCount = 5;

        public const int RandomUserToView = 5;


    }
}
