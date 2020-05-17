using System.Collections.Generic;
using FA.JustBlog.Core.Models;
using FA.JustBlog.DataAccessLayer.BaseService;

namespace FA.JustBlog.DataAccessLayer.Service.InterfaceService
{
    public interface ITagService : IBaseService<Tag>
    {
        Tag GetTagByUrlSlug(string urlSlug);

        IEnumerable<Tag> GetTagForCount(int size);
        void CalculateCountTags();

    }
}