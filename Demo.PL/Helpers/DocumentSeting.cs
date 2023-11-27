using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public static class DocumentSeting
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            string folderPath =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",folderName);
            string FileName = $"{Guid.NewGuid()},{file.FileName}";
            string FilePath = Path.Combine(folderPath, FileName);
            using var fs =new FileStream(FilePath, FileMode.Create);
            file.CopyTo(fs);
            return FileName;
        }
        public static void DeleteFile(string FolderName,string fileName)
        {
            string FullPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", FolderName, fileName);
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }

        }
    }
}
