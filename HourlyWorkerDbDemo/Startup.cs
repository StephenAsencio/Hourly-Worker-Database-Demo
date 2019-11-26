using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HourlyWorkerDbDemo.Startup))]
namespace HourlyWorkerDbDemo
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
