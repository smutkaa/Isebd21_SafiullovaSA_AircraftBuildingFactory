using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace AbsctractFactoryDatabaseImplement.Models
{
    public class Storage
    {
        public int Id { get; set; }
        [Required]
        public string StorageName { get; set; }
        [Required]
        public string ResponsiblePerson { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }

        [ForeignKey("StorageId")]
        public List<StorageComponent> StorageComponent { get; set; }
    }
}