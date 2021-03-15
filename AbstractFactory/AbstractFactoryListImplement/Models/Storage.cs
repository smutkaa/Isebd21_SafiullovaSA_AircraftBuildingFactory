using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryListImplement.Models
{
    public class Storage
    {
        public int Id { get; set; }

        public string StorageName { get; set; }

        public string ResponsiblePerson { get; set; }
        public DateTime DateCreate { get; set; }

        public Dictionary<int, int> StorageComponents { get; set; }
    }
}
