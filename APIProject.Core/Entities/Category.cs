﻿using APIProject.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Core.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
    }
}
