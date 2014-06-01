using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConvertEasy.Startup))]
namespace ConvertEasy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
