using System;
using System.Collections.Generic;
using FA.JustBlog.Core.GenericRepository;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repository.InterfaceRepositories;
using FA.JustBlog.DataAccessLayer.BaseService;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;

namespace FA.JustBlog.DataAccessLayer.Service
{
    public class TagService : BaseService<Tag>, ITagService
    {
        private ITagRepository TagRepository { get; }

        public TagService(ITagRepository tagRepository) : base(tagRepository)
        {
            TagRepository = tagRepository;
        }

        public Tag GetTagByUrlSlug(string urlSlug)
        {
            if (urlSlug is null)
            {
                throw  new  NullReferenceException();
            }

            return TagRepository.GetTagByUrlSlug(urlSlug);
        }

        public IEnumerable<Tag> GetTagForCount(int size)
        {
            return TagRepository.GetTagForCount(size);
        }

        public void CalculateCountTags()
        {
            TagRepository.CalculateCountTags();
        }
    }
}