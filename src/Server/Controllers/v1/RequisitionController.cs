using BlazorHero.CleanArchitecture.Application.Features.Requisitions.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1
{
    [ApiController]
    public class RequisitionController : BaseApiController<RequisitionController>
    {
        /// <summary>
        /// search a requisition
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery, Required] string numero)
        {
            return Ok(await _mediator.Send(new SearchRequisition.Request(numero)));
        }
    }
}
