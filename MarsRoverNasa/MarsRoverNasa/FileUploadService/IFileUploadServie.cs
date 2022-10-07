using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverNasa.FileUploadService
{
    public interface IFileUploadServie
    {
        string UploadFile(IFormFile file);
    }
}
