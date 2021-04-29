using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Data_.Dtos;
using Data_.Entities;
using Data_.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class FileService : IFileService
    {
        private IUnitOfWork UnitOfWork;
        public FileService(IUnitOfWork _UnitOfWork)
        {
            UnitOfWork = _UnitOfWork;
        }

        public async Task<Guid> AddFile(FileUploaded File, string PathToFolder,CancellationToken Cancel)
        {
            if (File?.Files.Length > 0)
            {
                if (!Directory.Exists(PathToFolder + "\\Upload\\"))
                {
                    Directory.CreateDirectory(PathToFolder + "\\Upload\\");
                }

                using (FileStream FileStream =
                    System.IO.File.Create(PathToFolder + "\\Upload\\" + File.Files.FileName))
                {
                    File.Files.CopyTo(FileStream);
                    FileStream.Flush();
                    return await AddToDbTable("\\Upload\\" + File.Files.FileName, Cancel);
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task<string> GetFileById(Guid Id, CancellationToken Cancel)
        {
            var File = await UnitOfWork.FileRepository.GetById(Id, Cancel);

            if (File == null)
                throw new ArgumentException();

            return File.Url;
        }

        public async Task<IEnumerable<string>> GetFiles(CancellationToken Cancel)
        {
            return (await UnitOfWork.FileRepository.GetAll(Cancel)).Select(File => File.Url);
        }

        private async Task<Guid> AddToDbTable(string Path, CancellationToken Cancel)
        {
            var File = new Data_.Entities.File
            {
                Id = new Guid(),
                Url = Path
            };

            await UnitOfWork.FileRepository.Create(File, Cancel);
            await UnitOfWork.SaveChangesAsync(Cancel);

            return File.Id;
        }

    }
}
