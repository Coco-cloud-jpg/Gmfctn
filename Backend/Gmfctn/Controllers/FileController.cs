using System;
using Data_;
using Data_.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Services.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Data_.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Gmfctn.Controllers
{
    [Route("api/files")]
    [ApiController]
    [Authorize]

    public class FileController : ControllerBase
    {
        private IFileService FileService;
        private IWebHostEnvironment Environment;
        public FileController(IFileService _FileService, IWebHostEnvironment _Environment)
        {
            FileService = _FileService;
            Environment = _Environment;
        }

        [HttpPost("add")]
        public async Task<ActionResult<string>> AddFile([FromForm]FileUploaded File, CancellationToken Cancel)
        {
            try
            {
                return Ok(await FileService.AddFile(File, Environment.WebRootPath, Cancel));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("get-by-id")]
        public async Task<ActionResult<string>> GetById(Guid Id, CancellationToken Cancel)
        {
            try
            {
                return Ok(await FileService.GetFileById(Id, Cancel));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("get-all")]
        public async Task<ActionResult<IEnumerable<string>>> GetAll(CancellationToken Cancel)
        {
            try
            {
                return Ok(await FileService.GetFiles(Cancel));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
