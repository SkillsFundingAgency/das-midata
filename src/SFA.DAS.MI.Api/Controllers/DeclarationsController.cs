using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MediatR;
using NLog;
using SFA.DAS.MI.Api.Attributes;
using SFA.DAS.MI.Application.Queries.GetDeclarations;

namespace SFA.DAS.MI.Api.Controllers
{
    [RoutePrefix("apprenticeship-levy/epaye/{empRef}/declarations")]
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
        public async Task<IHttpActionResult> GetDeclarations(string empRef)
        {
            var decodedEmpref = HttpUtility.UrlDecode(empRef);

            _logger.Info($"Declarations API called for {decodedEmpref}");

            var result = await _mediator.SendAsync(new GetDeclarationsRequest {EmpRef = decodedEmpref});

            if (result?.Declarations?.Declarations == null)
            {
                _logger.Info($"No declarations found for {empRef}");
                return NotFound();
            }
            
            return Ok(result.Declarations);
        }
    }
}
