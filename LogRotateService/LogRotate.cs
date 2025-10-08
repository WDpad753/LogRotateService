using BaseClass.Base.Interface;
using BaseClass.Config;
using BaseClass.ConsoleAppBase;
using BaseClass.JSON;
using BaseClass.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace LogRotateService
{
    internal class LogRotate : ServiceBase
    {
        private readonly IBaseProvider _provider;
        private readonly IBaseSettings _settings;
        private readonly ILogger logger;
        private ConfigHandler _configHandler;
        private JSONFileHandler jsonHandler;
        private Timer _runTimer;

        private int? _delay;
        private int mainRun = 0;

        public LogRotate(IBaseProvider provider) : base(provider)
        {
            _provider = provider;

            _configHandler = provider.GetItem<ConfigHandler>();
            _settings = provider.GetItem<IBaseSettings>();
            logger = provider.GetItem<ILogger>();
            jsonHandler = provider.GetItem<JSONFileHandler>();

            _runTimer = new Timer();
        }

        public override bool CanStart()
        {
            throw new NotImplementedException();
        }

        public override Task StartupTasks()
        {
            throw new NotImplementedException();
        }
    }
}
