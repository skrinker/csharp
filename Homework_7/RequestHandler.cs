using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using WebServer.Attributes;

namespace WebServer
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(HttpListenerRequest request, HttpListenerResponse response);
    }

    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        public virtual object Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request, response);
            }
            else
            {
                return null;
            }
        }
    }

    class StaticHandler : AbstractHandler
    {
        private string getContentType(string extension)
        {
            switch (extension)
            {
                case "htm":
                case "html":
                case "ico":
                    return "text/html";
                case "css":
                    return "text/stylesheet";
                case "js":
                    return "text/javascript";
                case "jpg":
                    return "image/jpeg";
                case "jpeg":
                case "png":
                case "gif":
                    return "image/" + extension;
                default:
                    if (extension.Length > 1)
                    {
                        return "application/" + extension.Substring(1);
                    }
                    return "application/unknown";
            }
        }
        private bool isStaticRequest(HttpListenerRequest request)
        {
            string requestedFilePath = request.Url.AbsolutePath.Substring(1);
            var splitted = requestedFilePath.Split(".");
            return requestedFilePath == "google" || requestedFilePath == "kttc" || requestedFilePath == "battle" || splitted.Length > 1 && splitted[1] != "";
        }
        public override object Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (isStaticRequest(request))
            {
                string rootFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string rootDirectory = Directory.GetParent(rootFolderPath).Parent.Parent.FullName;
                string requestedFilePath = request.Url.AbsolutePath.Substring(1);
                string staticFilesFolderPath = Path.Combine(rootDirectory, "StaticFiles");
                string fullPath = Path.Combine(staticFilesFolderPath, requestedFilePath);
                string contentType = "";
                byte[] buffer = new byte[] { };
                int statusCode = 404;
                try
                {
                    if (File.Exists(fullPath))
                    {
                        buffer = File.ReadAllBytes(fullPath);
                        contentType = getContentType(requestedFilePath.Split('.')[1]);
                        statusCode = 200;

                    }
                    if (Directory.Exists(fullPath))
                    {
                        buffer = File.ReadAllBytes($@"{fullPath}/index.html");
                        contentType = "text/html";
                        statusCode = 200;
                        Console.WriteLine(fullPath);
                    }
                }
                catch (Exception e)
                {
                    statusCode = 404;
                }

                response.ContentType = contentType;
                response.StatusCode = statusCode;
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
                return "";
            }
            else
            {
                return base.Handle(request, response);
            }
        }
    }
    class MethodsHandler : AbstractHandler
    {
        public override object Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            var result = true;
            string[] strParams = request.Url
                .Segments
                .Skip(2)
                .Select(s => s.Replace("/", ""))
                .ToArray();
            Console.WriteLine(strParams.Length > 0 ? strParams[0] : "");
            var assembly = Assembly.GetExecutingAssembly();
            var controller = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HttpControllerAttribute))).FirstOrDefault(c
                => c.Name.ToLower() == "accountscontroller");
            Console.WriteLine(controller);
            if (controller == null)
            {
                result = false;
            }

            var test = typeof(HttpControllerAttribute).Name;
            var method = controller.GetMethods().Where(t => t.GetCustomAttributes(true)
            .Any(attr => {
                var buffer = attr.GetType();
                return attr.GetType().Name.ToLower() == $"http{request.HttpMethod.ToLower()}attribute";
            }))
            .FirstOrDefault();

            if (method == null) {
                return "";
            }

            //object[] queryParams = method.GetParameters()
            //    .Select((p, i) => Convert.ChangeType(strParams[i], p.ParameterType))
            //    .ToArray();
            var ret = method.Invoke(Activator.CreateInstance(controller), new object[2] {request, response});

            if (!result)
            {
                return base.Handle(request, response);
            }
            return "";
        }
    }

}