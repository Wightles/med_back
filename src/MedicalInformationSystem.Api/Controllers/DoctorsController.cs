using MedicalInformationSystem.Application.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace MedicalInformationSystem.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public sealed class DoctorsController(
    IDoctorQueryService doctorQueryService,
    ILogger<DoctorsController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<DoctorListItemDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DoctorListItemDto>>> GetAll(
        [FromQuery] string? specialty,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Getting doctors with specialty filter {Specialty}",
            specialty ?? "not specified");

        var doctors = await doctorQueryService.GetAllAsync(specialty, cancellationToken);

        return Ok(doctors);
    }

    [HttpGet("specialties")]
    [ProducesResponseType<IReadOnlyList<string>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<string>>> GetSpecialties(
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting doctor specialties");

        var specialties = await doctorQueryService.GetSpecialtiesAsync(cancellationToken);

        return Ok(specialties);
    }
}
