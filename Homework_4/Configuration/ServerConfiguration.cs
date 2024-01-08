using Newtonsoft.Json;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace HttpServer.Configuration;

public static class ServerConfiguration
{
        private const string configName = "appsettings.json";

        static ServerConfiguration()
        {
            ApplyConfiguration();
        }
        
        public static AppSettingsConfig Config { get; private set; }
        
        private static void ApplyConfiguration()
        {
            try
            {
                var json = File.OpenText(configName).ReadToEnd();
                var obj = JsonConvert.DeserializeObject<AppSettingsConfig>(json);
                EnsureStaticFilePath(obj);
                Config = obj;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File {0} not found.", configName);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred while deserializing a file: {0}", ex.Message);
                throw;
            }
        }
        
        private static void EnsureStaticFilePath(AppSettingsConfig config)
        {
            try
            {
                if (!Directory.Exists(config.StaticPathFiles))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), config.StaticPathFiles));
                    Console.WriteLine($"Static folder created: {config.StaticPathFiles}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while static folder creating: {config.StaticPathFiles}");
                throw;
            }
        }
}