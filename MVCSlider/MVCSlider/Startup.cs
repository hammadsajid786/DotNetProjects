using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCSlider.Startup))]
namespace MVCSlider
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
