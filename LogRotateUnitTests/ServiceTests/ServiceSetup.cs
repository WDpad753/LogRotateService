using BaseClass.Base;
using BaseClass.Base.Interface;
using BaseClass.Config;
using BaseClass.Helper;
using BaseClass.JSON;
using BaseClass.Model;
using BaseLogger;
using System.Reflection.Metadata;

namespace LogRotateUnitTests.ServiceTests
{
    public class ServiceSetup
    {
        private IBaseSettings? baseConfig;
        private ILogger? logwriter;
        private ConfigHandler configReader;
        private JSONFileHandler jsonFileHandler;
        private EnvFileHandler envFileHandler;
        private EnvHandler envHandler;
        private XmlHandler xmlHandler;
        private string logpath;
        private string configPath;

        [SetUp]
        public void Setup()
        {
            string configpath = @$"{AppDomain.CurrentDomain.BaseDirectory}Config\service.config";

            logpath = @$"{AppDomain.CurrentDomain.BaseDirectory}TempLogs\";
            configPath = configpath;

            if (Directory.Exists(logpath))
            {
                Directory.Delete(logpath, true); // Ensure the log directory is clean before starting the test
            }

            logwriter = new Logger(configpath, logpath);

            baseConfig = new BaseSettings()
            {
                ConfigPath = configpath,
            };

            xmlHandler = new(logwriter, baseConfig);
            envFileHandler = new(logwriter, baseConfig);
            configReader = new(logwriter, baseConfig, xmlHandler, envFileHandler);
            jsonFileHandler = new(logwriter);
            envHandler = new(logwriter, baseConfig, envFileHandler);
        }

        [Test]
        public void SetupMonitor()
        {
            Assert.Pass();
        }
    }
}
