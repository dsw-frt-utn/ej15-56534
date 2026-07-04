using System;
using System.Collections.Generic;
using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Data.Interfaces;


namespace Dsw2026Ej15.Data.Persistence;

public class PersistenceInMemory : IPersistence
{
    private List<Doctor> _doctors = new();
    private List<Speciality> _specialities = new();

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }

    private void LoadSpecialities()
    {
        var json = File.ReadAllText("specialities.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true 
        };
        _specialities = JsonSerializer.Deserialize<List<Speciality>>(json, options);
    }
    public List<Doctor> GetDoctors() => _doctors;

    public Doctor GetDoctorById(Guid id)
        => _doctors.FirstOrDefault(d => d.Id == id);

    public void AddDoctor(Doctor doctor)
        => _doctors.Add(doctor);

    public List<Speciality> GetSpecialities()
        => _specialities;

    public Speciality GetSpecialityById(Guid id)
        => _specialities.FirstOrDefault(s => s.Id == id);

    public void DeactivateDoctor(Doctor doctor)
    {
        doctor.IsActive = false;
    }

}
