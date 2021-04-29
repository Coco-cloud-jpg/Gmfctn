using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Dtos
{
    public class FileUploaded
    {
        public IFormFile Files { get; set; }
    }
}
