using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

using SFA.DAS.MI.Domain.Configuration;
using SFA.DAS.MI.FileReader.DependencyResolution;
using SFA.DAS.MI.FileReader.Workers;
using SFA.DAS.MI.Infrastructure.DependencyResolution;
using StructureMap;

namespace SFA.DAS.MI.FileReader
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private IContainer _container;



        public override void Run()
        {
            Trace.TraceInformation("SFA.DAS.MI.FileReader is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            bool result = base.OnStart();

            Trace.TraceInformation("SFA.DAS.MI.FileReader has been started");

            _container = new Container(c =>
            {
                c.Policies.Add(new ConfigurationPolicy<MiFileReaderConfiguration>("SFA.DAS.MI.FileReader"));
                c.Policies.Add<LoggingPolicy>();
                c.AddRegistry<DefaultRegistry>();
            });

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("SFA.DAS.MI.FileReader is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("SFA.DAS.MI.FileReader has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                var fileReader = _container.GetInstance<IFileReaderWorker>();
                fileReader.Handle();
                
                await Task.Delay(1000);
            }
        }
    }
}
