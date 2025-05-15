using BlazorHero.CleanArchitecture.Application.Features.Requisitions;
using BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1
{
    [ApiController]
    public class SynchronizeRequisitionDccfController : BaseApiController<SynchronizeRequisitionDccfController>
    {
        /// <summary>
        /// Synchronize requisition dccf
        /// </summary>
        /// <param name="Requisition"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Requisitions.Import)]
        [HttpPost]
        public async Task<IActionResult> Synchronize([FromBody, Required] RequisitionResponse Requisition)
        {
            return Ok(await _mediator.Send(new SynchronizeRequisitionDccf.Command()
            {
                Requisition = Requisition
            }));
        }
    }
}
