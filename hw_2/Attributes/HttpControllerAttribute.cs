using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Attributes
{
    public class HttpControllerAttribute : Attribute
    {
        public bool canHandle(string controllerName, HttpListenerContext httpContext) {
            string[] strParams = httpContext.Request.Url
                .Segments
                .Skip(2)
                .Select(s => s.Replace("/", ""))
                .ToArray();

            var assembly = Assembly.GetExecutingAssembly();
            var controller = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HttpControllerAttribute))).FirstOrDefault(c
                => c.Name.ToLower() == controllerName.ToLower());

            if (controller == null) return false;

            var test = typeof(HttpControllerAttribute).Name;
            var method = controller.GetMethods().Where(t => t.GetCustomAttributes(true)
            .Any(attr => {
                var buffer = attr.GetType();
                    return attr.GetType().Name.ToLower() == $"http{httpContext.Request.HttpMethod.ToLower()}attribute";
                }))
            .FirstOrDefault();

            if (method == null) return false;

            object[] queryParams = method.GetParameters()
            .Select((p, i) => Convert.ChangeType(strParams[i], p.ParameterType))
            .ToArray();

            var ret = method.Invoke(Activator.CreateInstance(controller), queryParams);

            return true;
        }
    }
}
