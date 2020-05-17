using System;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.HtmlControls;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.UI.Helper
{
    public static class ActionLinkExtensions
    {
        //public static MvcHtmlString PostTagLink(this HtmlHelper helper, Tag tag)
        //{
        //    return helper.ActionLink(tag.Name, "PostsByTag", "Post", new { tagId = tag.Id });
        //}

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category)
        {
            if (category != null)
            {
                return helper.ActionLink(category.Name, "PostByCategory", "Post", new { categoryId = category.Id }, new { title = String.Format("Category {0}", category.Name) });
            }
            else
            {
                return helper.ActionLink("Nothing", "Index", "Home");
            }
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(tag.Name, "PostsByTag", "Post", new { tagId = tag.Id }, new { title = String.Format("See all posts in {0}", tag.Name) });
        }
    }
}