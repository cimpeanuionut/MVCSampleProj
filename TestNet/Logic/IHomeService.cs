using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestNet.Entities;

namespace TestNet.Logic
{
    public interface IHomeService
    {
        Task UploadFile(IFormFile file);
        Task<IEnumerable<FileData>> GetFilesAsync();
        Task SendError();
    }
}
