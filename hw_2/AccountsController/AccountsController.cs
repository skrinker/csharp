using System.Net;
using WebServer.Attributes;

namespace WebServer.AccountsController
{
    [HttpControllerAttribute]
    public class AccountsController
    {
        [HttpPost]
        public void add(HttpListenerRequest request, HttpListenerResponse response)
        {
            using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                string requestData = reader.ReadToEnd();
                // Добавьте здесь код для обработки данных формы
            }
            response.StatusCode = 200;
            response.OutputStream.Close();
        }
        public void delete()
        {

        }

        void update()
        {

        }
        [GetAttribute("select")]
        void select()
        {

        }

        void selectById()
        {

        }
    }
}
