//using WebServer;
//using System.ComponentModel.DataAnnotations;
//using System.Net;
//using System.Reflection;
//using System.Text;
//using System.Text.Json;

//HttpListener server = new HttpListener();


//string text = File.ReadAllText(@"./appsettings.json");
//var appSettings = JsonSerializer.Deserialize<AppSettings>(text);
//server.Prefixes.Add($"http://{appSettings.Address}:{appSettings.Port}/");
//Console.WriteLine($"http://{appSettings.Address}:{appSettings.Port}");

//server.Start(); // начинаем прослушивать входящие подключения


//while (true)
//{
//    try
//    {
//        var context = await server.GetContextAsync();
//        string requestedFilePath = context.Request.Url.LocalPath.Substring(1); 
//string rootFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
//string projectFolderPath = Directory.GetParent(rootFolderPath).Parent.Parent.FullName;
//string staticFilesFolderPath = Path.Combine(projectFolderPath, "StaticFiles");
//        string fullPath = Path.Combine(staticFilesFolderPath, requestedFilePath);
//        Console.WriteLine(fullPath);
//        var url = context.Request.Url;
//        Console.WriteLine(url);
//        Console.WriteLine(url.AbsolutePath);
//        var response = context.Response;
//        string htmlPage = File.ReadAllText($@"{fullPath}");
//        byte[] buffer = Encoding.UTF8.GetBytes(htmlPage);
//        response.ContentLength64 = buffer.Length;
//        using Stream output = response.OutputStream;
//        await output.WriteAsync(buffer);
//        await output.FlushAsync();

//        Console.WriteLine("Запрос обработан");
//    } catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//}

//server.Stop();

using WebServer;
using System.Reflection;

string rootFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string projectFolderPath = Directory.GetParent(rootFolderPath).Parent.Parent.FullName;
//string staticFilesFolderPath = Path.Combine(projectFolderPath, "StaticFiles");
MyDumbServer dumbServer = new MyDumbServer(projectFolderPath, "123", 5050);
dumbServer.Start(); 