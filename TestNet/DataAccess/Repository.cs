using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNet.Entities;

namespace TestNet.DataAccess
{
    public class Repository : IRepository
    {
        private readonly TestSqlContext db;

        public Repository(TestSqlContext db)
        {
            this.db = db;
        }

        public Task<IEnumerable<FileData>> GetAllAsync()
        {
            return Task.FromResult(db.FileData.AsEnumerable());
        }

        public async Task AddAsync(FileData data)
        {
            await db.FileData.AddAsync(data);
            await db.SaveChangesAsync();
        }
    }
}
