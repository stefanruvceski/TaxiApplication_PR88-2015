using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using TaxiApplication.Models.Klase;

[assembly: OwinStartup(typeof(TaxiApplication.Startup))]

namespace TaxiApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            DataBase.Run();
        }
    }
}
