using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.JustBlog.Core.GenericRepository;
using FA.JustBlog.Core.Models;
using FA.JustBlog.DataAccessLayer.BaseService;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;

namespace FA.JustBlog.DataAccessLayer.Service
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IGenericRepository<Category> genericRepository) : base(genericRepository)
        {
        }
    }
}
