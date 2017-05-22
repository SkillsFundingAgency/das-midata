using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NLog;
using SFA.DAS.MI.Domain.Data.Repositories;
using SFA.DAS.MI.Domain.Entities;

namespace SFA.DAS.MI.Application.Commands.PublishMiData
{
    public class PublishMiDataHandler : IAsyncNotificationHandler<PublishMiDataRequest>
    {
        private readonly IDeclarationRepository _declarationRepository;
        private readonly IFractionRepository _fractionRepository;
        private readonly ILogger _logger;

        public PublishMiDataHandler(IDeclarationRepository declarationRepository, IFractionRepository fractionRepository, ILogger logger)
        {
            _declarationRepository = declarationRepository;
            _fractionRepository = fractionRepository;
            _logger = logger;
        }
        public async Task Handle(PublishMiDataRequest notification)
        {
            DateTime cessationDate;
            decimal levyAllowanceForYear;
            decimal levyDueYtd;
            int payrollMonth;

            if (!DateTime.TryParse(notification.Data.CessationDate, out cessationDate))
            {
                cessationDate = DateTime.Now;
            }
            decimal.TryParse(notification.Data.LevyAllowanceForYear, out levyAllowanceForYear);
            decimal.TryParse(notification.Data.LevyDueYtd, out levyDueYtd);
            int.TryParse(notification.Data.PayrollMonth, out payrollMonth);

            var declaration = new Declaration()
            {
                EmpRef = notification.Data.EmpRef,
                CeasationDate = cessationDate,
                LevyAllowanceForYear = levyAllowanceForYear,
                LevyDueYtd = levyDueYtd,
                PayrollMonth = payrollMonth,
                PayrollYear = notification.Data.PayrollYear,
                SubmissionDate = notification.Data.SubmissionDate

            };

            await _declarationRepository.SaveDeclaration(declaration);


            decimal fractionAmount;
            decimal.TryParse(notification.Data.FractionAmount, out fractionAmount);

            var fraction = new Fraction()
            {
                Amount = fractionAmount,
                DateCalculated = notification.Data.FractionDateCalculated,
                EmpRef = notification.Data.EmpRef
            };

            await _fractionRepository.SaveFraction(fraction);


            
        }
    }
}
