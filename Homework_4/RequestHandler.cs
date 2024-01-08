using System.IO;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace WebServer
{
    class RequestHandler
    {
        private readonly string _rootDirectory;
        private readonly string _defaultPage;

        public RequestHandler(string rootDirectory, string defaultPage)
        {
            _rootDirectory = rootDirectory;
            _defaultPage = defaultPage;
        }

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
                    return  "text/javascript";
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

        public void handleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string requestedFilePath = request.Url.AbsolutePath.Substring(1);
            string staticFilesFolderPath = Path.Combine(_rootDirectory, "StaticFiles");
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
                } 
            } catch (Exception e)
            {
                statusCode = 404;
            }

            response.ContentType = contentType;
            response.StatusCode = statusCode;
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
    }
}