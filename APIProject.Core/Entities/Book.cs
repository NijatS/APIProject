using APIProject.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Core.Entities
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
