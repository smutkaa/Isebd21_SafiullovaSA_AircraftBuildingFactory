﻿using AbstractFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsctractFactoryDatabaseImplement.Models
{
    public class StorageComponent
    {
        public int Id { get; set; }


        public int StorageId { get; set; }

        public int ComponentId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Component Component { get; set; }

        public virtual Storage Storage { get; set; }
    }
}