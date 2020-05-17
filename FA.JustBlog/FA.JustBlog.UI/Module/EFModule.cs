using System.Data.Entity;
using Autofac;
using FA.JustBlog.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FA.JustBlog.UI.Module
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(ApplicationDbContext)).InstancePerLifetimeScope();
            //builder.RegisterType(typeof(RepositoryModule)).As(typeof(ApplicationDbContext)).InstancePerRequest();
        }

    }
}