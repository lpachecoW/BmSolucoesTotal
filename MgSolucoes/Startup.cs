using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MgSolucoes.Startup))]
namespace MgSolucoes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
