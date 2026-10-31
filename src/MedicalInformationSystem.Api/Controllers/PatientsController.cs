using MedicalInformationSystem.Application.Patients;
using Microsoft.AspNetCore.Mvc;

namespace MedicalInformationSystem.Api.Controllers;

[ApiController]
[Route("api/patients")]
public sealed class PatientsController(
    IPatientQueryService patientQueryService,
    ILogger<PatientsController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<PatientListItemDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PatientListItemDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all patients");

        var patients = await patientQueryService.GetAllAsync(cancellationToken);

        return Ok(patients);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<PatientDetailsDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PatientDetailsDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting patient {PatientId}", id);

        var patient = await patientQueryService.GetByIdAsync(id, cancellationToken);

        if (patient is not null)
        {
            return Ok(patient);
        }

        logger.LogWarning("Patient {PatientId} was not found", id);

        return NotFound(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Patient not found",
            Detail = $"Patient with id '{id}' was not found."
        });
    }
}
