using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Repository.InterfaceRepositories
{
    public interface ITagManagementRepository
    {
        Tag GetTagByUrlSlug(string urlSlug);

        IEnumerable<Tag> GetTagForCount(int size);
        void CalculateCountTags();
    }
}
