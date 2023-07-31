using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Service.Dtos.Books
{
    public class BookUpdateDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
        public IFormFile file { get; set; }
        public int CategoryId { get; set; }
    }
}
