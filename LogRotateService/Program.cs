using BaseClass.Base;
using BaseClass.Base.Interface;
using BaseClass.Config;
using BaseClass.Helper;
using BaseClass.JSON;
using BaseClass.Model;
using BaseClass.Service.HostBuilderBase;
using BaseLogger;
using ILogger = BaseLogger.ILogger;

namespace LogRotateService
{
    public class Program
    {
        public static BaseSettings? baseSettings { get; set; }

        public static async Task Main(string[] args)
        {
            // Get the current directory (where your executable is located):
            string currentDirectory = Directory.GetCurrentDirectory();
            string currentDirectory2 = AppDomain.CurrentDomain.BaseDirectory;

            string configFilePath = Path.Combine(currentDirectory2, "Config");
            string[] files = (string[])Directory.GetFiles(configFilePath, "*.config");
            bool val = Directory.Exists(configFilePath);

            // Double check
            if (!Directory.Exists(configFilePath) || files.Count() < 0)
            {
                throw new Exception("Either the Config File Path does not exist or there are different Configs in the assigned path.");
            }

            string configFile = files[0];
            //string logFilePath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "tmp");
            string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "tmp");

            IHost host = Host.CreateDefaultBuilder(args).ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);
            }).CreateServiceBase<LogRotate, Worker>().Build();

            var provider = host.Services.GetRequiredService<IBaseProvider>();

            baseSettings = new BaseSettings
            {
                ConfigPath = configFile,
            };

            provider.RegisterInstance<IBaseSettings>(baseSettings);
            provider.RegisterInstance<ILogger>(new Logger(configFile, logFilePath));
            provider.RegisterInstance<ValueCollector<string>>(new ValueCollector<string>("Log Rotate Service", "ServiceName"));
            provider.RegisterInstance<ValueCollector<DatabaseMode>>(new ValueCollector<DatabaseMode>(DatabaseMode.None, "DatabaseMode"));
            provider.RegisterSingleton<EnvHandler>();
            provider.RegisterSingleton<XmlHandler>();
            provider.RegisterSingleton<EnvFileHandler>();
            provider.RegisterSingleton<ConfigHandler>();
            provider.RegisterSingleton<JSONFileHandler>();

            await host.RunAsync();
        }
    }
}