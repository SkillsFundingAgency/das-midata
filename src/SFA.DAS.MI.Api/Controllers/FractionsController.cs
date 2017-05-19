using System;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using NLog;
using SFA.DAS.MI.Api.Attributes;
using SFA.DAS.MI.Application;
using SFA.DAS.MI.Application.Queries.GetFractions;

namespace SFA.DAS.MI.Api.Controllers
{
    
    [RoutePrefix("apprenticeship-levy/epaye/{empRef1}/{empRef2}/fractions")]
    public class FractionsController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public FractionsController(ILogger logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Route("", Name = "GetFractions")]
        [ApiAuthorize(Roles = "ReadLevy")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFractions(string empRef1, string empRef2)
        {
            var decodedEmpref = $"{empRef1}/{empRef2}";

            _logger.Info($"Fractions API called for {decodedEmpref}");
            try
            {
                var result = await _mediator.SendAsync(new GetFractionsRequest {EmpRef = decodedEmpref});

                if (result?.Fractions?.FractionCalculations == null)
                {
                    _logger.Info($"Fractions not found for {decodedEmpref}");
                    return NotFound();
                }

                return Ok(result.Fractions);
            }
            catch (InvalidRequestException ex)
            {
                _logger.Info(ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return InternalServerError();
            }
            
        }
    }
}
