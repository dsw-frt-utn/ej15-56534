using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Api.DTOs;
using Dsw2026Ej15.Api.Exceptions;
using Dsw2026Ej15.Data.Interfaces;
using Dsw2026Ej15.Domain.Entities;
namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    // POST api/doctors
    [HttpPost]
    public IActionResult Create([FromBody] CreateDoctorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Name es requerido");

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("LicenseNumber es requerido");

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
            throw new ValidationException("La especialidad no existe");

        var doctor = new Doctor
        {
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            IsActive = true,
            Speciality = speciality
        };

        _persistence.AddDoctor(doctor);
        return StatusCode(201);
    }

    // GET api/doctors
    [HttpGet]
    public IActionResult GetAll()
    {
        var doctors = _persistence.GetDoctors()
            .Where(d => d.IsActive)
            .Select(d => new DoctorResponse
            {
                Name = d.Name,
                LicenseNumber = d.LicenseNumber,
                SpecialityName = d.Speciality.Name
            });

        return Ok(doctors);
    }

    // GET api/doctors/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor is null || !doctor.IsActive)
            return NotFound();

        return Ok(new DoctorResponse
        {
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        });
    }

    // DELETE api/doctors/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor is null || !doctor.IsActive)
            return NotFound();

        doctor.IsActive = false;
        return NoContent();
    }
}
