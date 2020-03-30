using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Registration;

namespace Pumpkin.Core
{
    public static class CallInstaller
    {
        private static IEnumerable<Type> AllTypes
        {
            get { return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()); }
        }

        public static void NeedToInstallConfig(this IServiceCollection services)
        {
            foreach (var item in AllTypes
                .Where(it => !(it.IsAbstract || it.IsInterface)
                             && typeof(INeedToInstall).IsAssignableFrom(it)))
            {
                var service = (INeedToInstall) Activator.CreateInstance(item);

                service.Install(services);
            }
        }
    }
}