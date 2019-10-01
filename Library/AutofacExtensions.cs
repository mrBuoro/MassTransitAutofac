using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public static class AutofacExtensions
    {
        public static AutofacServiceProvider StdAutofac(this IServiceCollection services)
        {
            // Autofac: a replacement for the default service container
            // - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#default-service-container-replacement
            // - https://autofac.org/
            // - https://autofac.readthedocs.io/en/latest/getting-started/index.html
            // - https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html

            var autofac = new ContainerBuilder();
            autofac.Populate(services);

            return new AutofacServiceProvider(autofac.Build());
        }

    }
}
