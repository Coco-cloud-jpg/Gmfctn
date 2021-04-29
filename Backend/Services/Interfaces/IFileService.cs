using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data_.Dtos;

namespace Services.Interfaces
{
    public interface IFileService
    {
        Task<Guid> AddFile(FileUploaded File, string PathToFolder, CancellationToken Cancel);
        Task<string> GetFileById(Guid Id, CancellationToken Cancel);
        Task<IEnumerable<string>> GetFiles(CancellationToken Cancel);
    }
}
