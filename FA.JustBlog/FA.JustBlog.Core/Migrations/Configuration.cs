using System.IO;

namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FA.JustBlog.Core.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FA.JustBlog.Core.Models.ApplicationDbContext";
        }

        protected override void Seed(FA.JustBlog.Core.Models.ApplicationDbContext context)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\Debug", string.Empty);
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + "\\JustBlogScript.sql"));
        }
    }
}
