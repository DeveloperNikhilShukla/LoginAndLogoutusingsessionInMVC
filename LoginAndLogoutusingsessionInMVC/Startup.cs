using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginAndLogoutusingsessionInMVC.Startup))]
namespace LoginAndLogoutusingsessionInMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
