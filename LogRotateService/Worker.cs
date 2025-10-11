using BaseClass.Base.Interface;
using BaseClass.ConsoleAppBase;
using BaseClass.Service;
using BaseClass.Service.BaseWorker;
using BaseLogger;
using Microsoft.Extensions.Hosting;
using ILogger = BaseLogger.ILogger;

namespace LogRotateService
{
    public class Worker : ServiceWorkerBase
    {
        private readonly ServiceBase _serviceBase;
        private readonly IHostApplicationLifetime _lifeTime;
        private readonly ILogger _logger;

        public Worker(IHostApplicationLifetime lifetime, IBaseProvider provider, ServiceBase service) : base(lifetime, service)
        {
            _serviceBase = service;
            _lifeTime = lifetime;

            _logger = provider.GetItem<ILogger>();
        }

        protected override async Task ExecuteTaskAsync(CancellationToken stoppingToken)
        {
            if (_serviceBase.CanStart())
            {
                await _serviceBase.Start(stoppingToken);
            }
            else
            {
                _logger.Error("Service cannot start. Stopping host...");
                _lifeTime.StopApplication();
                await StopAsync(_lifeTime.ApplicationStopping);
            }
        }
    }
}
