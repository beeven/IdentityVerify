using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace IdentityVerifyLib
{
    public static class Extensions
    {
        public static void AddIdentityVerifyService(this IServiceCollection collection, Action<Options> configureOptions)
        {
            var options = new Options();
            configureOptions(options);
            collection.AddSingleton(new TokenService(options));
            collection.AddScoped<IIdentityVerifyService>(provider =>
            {
                var tokenService = provider.GetService<TokenService>();
                return new IdentityVerifyServiceImpl(tokenService, options);
            });
        }
    }
}
