using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class MyDumbServer
    {
        private readonly string _rootDirectory;
        private readonly string _defaultPage;
        private AppSettingsConfig _config = Configuration.ServerConfiguration.Config;
        private readonly int _port;
        private DumbLogging _log;
        public MyDumbServer(string rootDirectory, string defaultPage, int port)
        {
            _rootDirectory = rootDirectory;
            _defaultPage = defaultPage;
            _port = port;
            _log = new DumbLogging();
        }

        public void Start()
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add($"http://localhost:{_config.Port}/");
                listener.Start();
                _log.successLog($"Server is running on http://localhost:{_config.Port}/");

                while (true)
                {
                    var context = listener.GetContext();
                    var request = context.Request;
                    var response = context.Response;

                    var handler = new RequestHandler(_rootDirectory, _defaultPage);

                    handler.handleRequest(request, response);
                }
            }
        }
    }
}
