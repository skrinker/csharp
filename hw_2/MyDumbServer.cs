using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShizaEbannaya
{
    class MyDumbServer
    {
        private readonly string _rootDirectory;
        private readonly string _defaultPage;
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
                listener.Prefixes.Add($"http://localhost:{_port}/");
                listener.Start();
                _log.successLog($"Server is running on http://localhost:{_port}/");

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
