using System.Reflection;
using Autofac;

namespace FA.JustBlog.UI.Module
{
    public class ServiceModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(Assembly.Load("FA.JustBlog.DataAccessLayer"))

                .Where(t => t.Name.EndsWith("Service"))

                .AsImplementedInterfaces()

                .InstancePerLifetimeScope();

        }

    }
}