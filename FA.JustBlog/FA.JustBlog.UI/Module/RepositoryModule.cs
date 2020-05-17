using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;

namespace FA.JustBlog.UI.Module
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("FA.JustBlog.Core"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}