using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Extensions
{
    public static class Extension
    {
        public static bool isImage(this IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            return false;
        }

        public static bool isSmallerThan(this IFormFile file,int bytes)
        {
            if (file.Length < bytes)
            {
                return true;
            }
            return false;
        }

        public static async Task<string> Upload(this IFormFile file,string path)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string finalPath = Path.Combine(path, fileName);

            using(FileStream stream =new FileStream(finalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
