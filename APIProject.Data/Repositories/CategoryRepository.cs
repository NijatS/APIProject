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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(APIProjectDbContext context) : base(context)
        {
        }
    }
}
