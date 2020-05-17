using FA.JustBlog.Core.GenericRepository;
using FA.JustBlog.Core.Models;
using System.Collections.Generic;

namespace FA.JustBlog.Core.Repository.InterfaceRepositories
{
    public interface ITagRepository : ITagManagementRepository, IGenericRepository<Tag>
    {
    }

}
