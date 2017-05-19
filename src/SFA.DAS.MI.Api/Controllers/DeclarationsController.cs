using System;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using NLog;
using SFA.DAS.MI.Api.Attributes;
using SFA.DAS.MI.Application;
using SFA.DAS.MI.Application.Queries.GetDeclarations;

namespace SFA.DAS.MI.Api.Controllers
{
    
    [RoutePrefix("apprenticeship-levy/epaye/{empRef1}/{empRef2}/declarations")]
    public class DeclarationsController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public DeclarationsController(ILogger logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [Route("", Name = "GetDeclarations")]
        [ApiAuthorize(Roles = "ReadLevy")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDeclarations(string empRef1, string empRef2)
        {
            var decodedEmpref = $"{empRef1}/{empRef2}";

            _logger.Info($"Declarations API called for {decodedEmpref}");

            try
            {
                var result = await _mediator.SendAsync(new GetDeclarationsRequest {EmpRef = decodedEmpref});

                if (result?.Declarations?.Declarations == null)
                {
                    _logger.Info($"No declarations found for {decodedEmpref}");
                    return NotFound();
                }

                return Ok(result.Declarations);
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
