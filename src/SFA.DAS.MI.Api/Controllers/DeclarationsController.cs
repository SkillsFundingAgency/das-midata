using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using NLog;
using SFA.DAS.MI.Api.Attributes;

namespace SFA.DAS.MI.Api.Controllers
{
    [RoutePrefix("apprenticeship-levy/epaye/{empRef}/declarations")]
    public class DeclarationsController : ApiController
    {
        private readonly ILogger _logger;

        public DeclarationsController(ILogger logger, IMediator mediator)
        {
            _logger = logger;
        }


        [Route("", Name = "GetDeclarations")]
        [ApiAuthorize(Roles = "ReadLevy")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDeclarations(string empRef)
        {
            _logger.Info($"Declarations API called for {empRef}");
            return Ok();
        }
    }
}
