using System.Collections.Generic;
using System.Threading.Tasks;
using TestNet.Entities;

namespace TestNet.DataAccess
{
    public interface IRepository
    {
        Task<IEnumerable<FileData>> GetAllAsync();
        Task AddAsync(FileData data);
    }
}
