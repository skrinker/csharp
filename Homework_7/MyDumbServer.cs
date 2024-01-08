using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServer.AccountsController;
using WebServer.Attributes;
using WebServer.Services;

namespace WebServer
{
    class MyDumbServer
    {
        private readonly int _port;
        private DumbLogging _log;
        public MyDumbServer(int port)
        {
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
                var staticHandler = new StaticHandler();
                var acc = new HttpControllerAttribute();
                staticHandler.SetNext(new MethodsHandler());
                while (true)
                {
                    var context = listener.GetContext();
                    var request = context.Request;
                    var response = context.Response;
                    
                    var result = staticHandler.Handle(request, response);
                    
                    if (result != null)
                    {
                        _log.infoLog($"Successfully handled request {request.Url}");
                    } else
                    {
                        _log.failureLog($"Error request {request.Url}");
                    }
                }
            }
        }
    }
}
