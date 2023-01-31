using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignerInterceptor;

public static class Func
{
    public static T GetService<T>(this IServiceProvider prov) where T : class => prov.GetService(typeof(T)) as T;

    public async static Task<T> GetServiceAsync<T>(this IAsyncServiceProvider prov) where T : class => (await prov.GetServiceAsync(typeof(T))) as T;
}
