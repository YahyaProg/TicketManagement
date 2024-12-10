
using Core.GenericResultModel;
using Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {

            #region AddResources
            //read resource and add it to static model ResourceConfig
            ResourceManager rmEn = new("Core.Resources.Resource-en", Assembly.GetExecutingAssembly());
            ResourceConfig.ResourcesEn = rmEn.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            ResourceManager rmFa = new("Core.Resources.Resource-fa", Assembly.GetExecutingAssembly());
            ResourceConfig.ResourcesFa = rmFa.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            #endregion

            services.AddScoped<IUserHelper, UserHelper>();

            return services;
        }
    }
}
