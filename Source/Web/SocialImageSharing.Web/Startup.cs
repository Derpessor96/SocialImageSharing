using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialImageSharing.Web.Startup))]
namespace SocialImageSharing.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
