using System;
using System.Collections.Generic;

namespace TestNet.Entities
{
    public partial class FileData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? UploadDate { get; set; }
        public string ArchiveType { get; set; }
        public string Status { get; set; }
    }
}
