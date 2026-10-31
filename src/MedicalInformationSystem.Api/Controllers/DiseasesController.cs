using MedicalInformationSystem.Application.Diseases;
using Microsoft.AspNetCore.Mvc;

namespace MedicalInformationSystem.Api.Controllers;

[ApiController]
[Route("api/diseases")]
public sealed class DiseasesController(
    IDiseaseQueryService diseaseQueryService,
    ILogger<DiseasesController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<DiseaseListItemDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DiseaseListItemDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all diseases");

        var diseases = await diseaseQueryService.GetAllAsync(cancellationToken);

        return Ok(diseases);
    }
}
