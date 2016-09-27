using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KursMataUangRupiah.Startup))]
namespace KursMataUangRupiah
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
