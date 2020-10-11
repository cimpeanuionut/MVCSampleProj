using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using TestNet.DataAccess;
using TestNet.Entities;

namespace TestNet.Logic
{
    public class HomeService : IHomeService
    {      
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IRepository rep;

        public HomeService(IWebHostEnvironment hostingEnvironment, IRepository rep)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.rep = rep;
        }

        public async Task<IEnumerable<FileData>> GetFilesAsync()
        {
            return await rep.GetAllAsync();
        }
        
        public async Task UploadFile(IFormFile file)
        {
            var path = hostingEnvironment.WebRootPath + "\\Archives";

            var tmpId = Guid.NewGuid();
            var tmpPath = path + "\\" + tmpId.ToString();

            if (!Directory.Exists(tmpPath))
                Directory.CreateDirectory(tmpPath);

            using (var fileStream = new FileStream(tmpPath + "\\" + file.FileName, FileMode.Create))
                await file.CopyToAsync(fileStream);

            var fileData = new FileData();
            fileData.ArchiveType = ".zip";
            fileData.UploadDate = DateTime.Today;
            fileData.Name = tmpId.ToString() + file.FileName.Replace(Path.GetExtension(file.FileName), string.Empty);
            fileData.Status = "success";

            using (ZipArchive newFile = ZipFile.Open(path + "\\" + fileData.Name + fileData.ArchiveType, ZipArchiveMode.Create))            
                foreach (string f in Directory.GetFiles(tmpPath))                
                    newFile.CreateEntryFromFile(f, Path.GetFileName(f));            

            if (Directory.Exists(tmpPath))
                Directory.Delete(tmpPath, true);

            await rep.AddAsync(fileData);
        }

        public async Task SendError()
        {
            var fileData = new FileData();
            fileData.Status = "eroare";

            await rep.AddAsync(fileData);
        }
    }
}
