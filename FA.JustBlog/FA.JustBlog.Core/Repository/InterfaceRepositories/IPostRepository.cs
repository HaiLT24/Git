using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using FA.JustBlog.Core.GenericRepository;

namespace FA.JustBlog.Core.Repository.InterfaceRepositories
{
    public interface IPostRepository : IGenericRepository<Post>, IManagementPostRepository
    {
        
    }
}
