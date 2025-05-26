using BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands;
using BlazorHero.CleanArchitecture.Application.Features.Requisitions.Queries;
using BlazorHero.CleanArchitecture.Infrastructure.Services;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1
{
    [ApiController]
    public class SuiviRequisitionController : BaseApiController<SuiviRequisitionController>
    {
        private readonly ISuiviRequisitionService _suiviRequisitionService;
        public SuiviRequisitionController(ISuiviRequisitionService suiviRequisitionService)
        {
            _suiviRequisitionService = suiviRequisitionService;
        }

        /// <summary>
        /// Get all requisitions by pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Requisitions.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int size, string search)
        {
            return Ok(await _mediator.Send(new GetAllRequisitions.Request()
            {
                PageNumber = page,
                PageSize = size,
                SearchString = search
            }));
        }

        /// <summary>
        /// Export a model
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Requisitions.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            return Ok(await _suiviRequisitionService.ExportToExcelAsync());
        }

        /// <summary>
        /// Update requisition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Requisitions.Update)]
        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateRequisition.Command command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Import old requisitions
        /// </summary>
        /// <param name="importCommand"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Requisitions.Import)]
        [HttpPost("import")]
        public async Task<IActionResult> Import(ImportRequisitions.Command importCommand)
        {
            return Ok(await _mediator.Send(importCommand));
        }
    }
}
