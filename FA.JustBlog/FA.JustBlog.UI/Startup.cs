using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(FA.JustBlog.UI.Startup))]
namespace FA.JustBlog.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
