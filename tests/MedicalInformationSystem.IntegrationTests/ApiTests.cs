namespace MedicalInformationSystem.IntegrationTests;

[Collection(AppHostCollection.Name)]
public sealed class ApiTests(AppHostFixture fixture)
{
    [Fact]
    public async Task GetPatientsReturnsSeededPatients()
    {
        var patients = await fixture.ApiClient
            .GetFromJsonAsync<List<PatientListItem>>("/api/patients");

        Assert.NotNull(patients);
        Assert.Equal(2, patients.Count);
        Assert.Contains(patients, patient => patient.MedicalPolicyNumber == "5400123456789012");
    }

    [Fact]
    public async Task GetPatientByIdReturnsPatientWithDiagnosis()
    {
        var patient = await fixture.ApiClient.GetFromJsonAsync<PatientDetails>(
            "/api/patients/65da26b0-3a12-4f8e-8509-9c7d94591ab9");

        Assert.NotNull(patient);
        Assert.Equal("Female", patient.Gender);

        var diagnosis = Assert.Single(patient.Diagnoses);
        Assert.Equal("I10", diagnosis.Icd10Code);
        Assert.Equal("Кардиолог", diagnosis.DoctorSpecialty);
    }

    [Fact]
    public async Task GetMissingPatientReturnsNotFound()
    {
        var response = await fixture.ApiClient.GetAsync(
            "/api/patients/00000000-0000-0000-0000-000000000000");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetailsResponse>();
        Assert.NotNull(problemDetails);
        Assert.Equal(404, problemDetails.Status);
    }

    [Fact]
    public async Task GetDoctorsFiltersBySpecialtyIgnoringCase()
    {
        var specialty = Uri.EscapeDataString("кардиолог");
        var doctors = await fixture.ApiClient
            .GetFromJsonAsync<List<DoctorListItem>>($"/api/doctors?specialty={specialty}");

        Assert.NotNull(doctors);
        var doctor = Assert.Single(doctors);
        Assert.Equal("Кардиолог", doctor.Specialty);

        var specialties = await fixture.ApiClient
            .GetFromJsonAsync<List<string>>("/api/doctors/specialties");

        Assert.NotNull(specialties);
        Assert.Contains("Терапевт", specialties);
    }

    [Fact]
    public async Task GetDiseasesReturnsIcd10Directory()
    {
        var diseases = await fixture.ApiClient
            .GetFromJsonAsync<List<DiseaseListItem>>("/api/diseases");

        Assert.NotNull(diseases);
        Assert.Equal(2, diseases.Count);
        Assert.Contains(diseases, disease => disease.Icd10Code == "I10" && disease.IsChronic);
    }

    private sealed record PatientListItem(string MedicalPolicyNumber);

    private sealed record PatientDetails(
        string Gender,
        IReadOnlyList<DiagnosisItem> Diagnoses);

    private sealed record DiagnosisItem(
        string Icd10Code,
        string DoctorSpecialty);

    private sealed record ProblemDetailsResponse(int Status);

    private sealed record DoctorListItem(string Specialty);

    private sealed record DiseaseListItem(
        string Icd10Code,
        bool IsChronic);
}
