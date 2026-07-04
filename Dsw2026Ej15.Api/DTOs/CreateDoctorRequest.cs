namespace Dsw2026Ej15.Api.DTOs;

public class CreateDoctorRequest
{
    public String? Name { get; set; }
    public String? LicenseNumber { get; set; }
    public Guid SpecialityId { get; set; }
}
