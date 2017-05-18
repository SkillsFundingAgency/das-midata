using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using SFA.DAS.MI.Api.Attributes;

namespace SFA.DAS.MI.Api.Controllers
{
    [RoutePrefix("apprenticeship-levy/epaye/{empRef}/fractions")]
    public class FractionsController : ApiController
    {
        private readonly ILogger _logger;

        public FractionsController(ILogger logger)
        {
            _logger = logger;
        }

        [Route("", Name = "GetFractions")]
        [ApiAuthorize(Roles = "ReadLevy")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFractions(string empRef)
        {

            _logger.Info($"Fractions API called for {empRef}");

            return Ok();
        }
    }
}
