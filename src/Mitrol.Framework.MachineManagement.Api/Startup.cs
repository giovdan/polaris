using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoDbVsEF.Api
{
    public class Startup
    {
        #region private methods

        //Set authorization polices
        private void SetAuthorizationPolicies(IServiceCollection services)
        {
            
        }


        #endregion private methods

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private IServiceProvider RegisterServices(IServiceCollection services)
        {
            return null;
        }
    }
}
