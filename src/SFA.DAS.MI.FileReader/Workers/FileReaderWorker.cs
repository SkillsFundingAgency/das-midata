using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NLog;
using SFA.DAS.MI.Application.Commands.PublishMiData;
using SFA.DAS.MI.Application.Queries.GetMiData;
using SFA.DAS.MI.Domain.Configuration;
using SFA.DAS.MI.Domain.Models.MiData;

namespace SFA.DAS.MI.FileReader.Workers
{
    public class FileReaderWorker : IFileReaderWorker
    {
        private readonly MiFileReaderConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FileReaderWorker(MiFileReaderConfiguration configuration, IMediator mediator, ILogger logger)
        {
            _configuration = configuration;
            _mediator = mediator;
            _logger = logger;
        }
        public async void Handle()
        {
            _logger.Info("Started File Reader Worker");

            var lockFilePath = _configuration.FileSystemPath + "\\lock.tmp";
            var locked = File.Exists(lockFilePath);

            try
            {
                if (!locked)
                {

                    File.WriteAllText(lockFilePath, "FileReaderWorker lock file - boom");
                    var files = Directory.GetFiles(_configuration.FileSystemPath);

                    

                    foreach (var file in Directory.GetFiles(_configuration.FileSystemPath))
                    {
                        if (file.EndsWith(".tmp")) return;
                        var miData = _mediator.Send(new GetMiDataRequest() { FilePath = file });
                        var fileInfo = new FileInfo(file);

                        for (int i = 0; i < miData.Data.Count; i++)
                        {
                            var row = miData.Data[i];
                            await _mediator.PublishAsync(new PublishMiDataRequest() { Data = row });
                        }
  

                        _logger.Info("Done, moving file to archive.");
                        File.Move(file, _configuration.FileSystemPath + "\\archive\\" + fileInfo.Name);
                        _logger.Info("File moved");

                        File.Delete(lockFilePath);
                        _logger.Info("Lock file released");
                    }
      
                }
                else
                {
                    _logger.Info("Lock file exists so nothing to do.");
                }
            
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                File.Delete(lockFilePath);
            }
        }
    }


    internal interface IFileReaderWorker
    {
        void Handle();
    }


}

