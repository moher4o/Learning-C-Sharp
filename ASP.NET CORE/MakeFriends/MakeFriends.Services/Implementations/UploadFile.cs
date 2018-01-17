using MakeFriends.Data;
using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MakeFriends.Services.Implementations

{
    public class UploadFile : IUploadFile
    {
        private int size = 500;
        private int quality = 75;

        public bool DeleteUserPhoto(string photopath)
        {
            FileInfo file = new FileInfo(Environment.CurrentDirectory + photopath);
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            return false;
        }

        public bool DeleteUserDirectory(string userId)
        {
            string globalPath = Environment.CurrentDirectory;

            if (Directory.Exists(globalPath + "/" + DataConstants.UserPhotoSubDirectory + "/" + userId))
            {
                Directory.Delete(globalPath + "/" + DataConstants.UserPhotoSubDirectory + "/" + userId, true);
            }
                return true;
        }

        public bool ProcessPhoto(Stream inputStream, string userId, string photoName)
        {
        var settings = new ProcessImageSettings()
        {
            Width = size,
            Height = size,
            ResizeMode = CropScaleMode.Crop,
            SaveFormat = FileFormat.Jpeg,
            JpegQuality = quality,
            JpegSubsampleMode = ChromaSubsampleMode.Subsample420
        };
        

            string globalPath = Environment.CurrentDirectory;

            if (photoName == DataConstants.FirstUserPhotoName)
            {
                Directory.CreateDirectory(globalPath + "/" + DataConstants.UserPhotoSubDirectory + "/" + userId);
            }

            using (var output = new FileStream(globalPath + "/" + DataConstants.UserPhotoSubDirectory + "/" + userId + "/" + photoName, FileMode.Create))
            {
                try
                {
                    MagicImageProcessor.ProcessImage(inputStream, output, settings);
                }
                catch (Exception)
                {

                    return false;
                }
                
            }
            return true;
        }
    }
}
