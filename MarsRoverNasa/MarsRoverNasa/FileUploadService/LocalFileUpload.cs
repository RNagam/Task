using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverNasa.FileUploadService
{
    public class LocalFileUpload : IFileUploadServie
    {
        private readonly IWebHostEnvironment environment;

        public LocalFileUpload(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public string UploadFile(IFormFile file)
        {
            var filePath = Path.Combine(environment.WebRootPath, "UploadFiles", file.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            //fileStream.Position = 0;
            file.CopyTo(fileStream);
            fileStream.Flush();           
            return filePath;
        }
    }
}
