using Microsoft.Owin;
using Owin;
using layeredFundRaiserSystem;
using Microsoft.AspNet.SignalR;

namespace layeredFundRaiserSystem
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}