using APIProject.Core.Entities;
using APIProject.Core.Repositories;
using APIProject.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(APIProjectDbContext context) : base(context)
        {
        }
    }
}
