using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SFA.DAS.MI.Api.Attributes;

namespace SFA.DAS.MI.Api.Controllers
{
    [RoutePrefix("apprenticeship-levy/epaye/{empRef}/declarations")]
    public class DeclarationsController : ApiController
    {
        public DeclarationsController()
        {
            
        }


        [Route("", Name = "GetDeclarations")]
        [ApiAuthorize(Roles = "ReadLevy")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDeclarations(string empRef)
        {
            return Ok();
        }
    }
}
