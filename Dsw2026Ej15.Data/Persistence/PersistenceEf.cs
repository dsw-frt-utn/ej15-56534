using Dsw2026Ej15.Data.Context;
using Dsw2026Ej15.Data.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.Persistence;

public class PersistenceEf : IPersistence
{
    private readonly AppDbContext _context;

    public PersistenceEf(AppDbContext context)
    {
        _context = context;
    }

    public List<Doctor> GetDoctors() =>
        _context.Doctors.Include(d => d.Speciality).ToList();

    public Doctor GetDoctorById(Guid id) =>
        _context.Doctors.Include(d => d.Speciality)
                        .FirstOrDefault(d => d.Id == id);

    public void AddDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public List<Speciality> GetSpecialities() =>
        _context.Specialities.ToList();

    public Speciality GetSpecialityById(Guid id) =>
        _context.Specialities.FirstOrDefault(s => s.Id == id);

    public void DeactivateDoctor(Doctor doctor) // ← NUEVO
    {
        doctor.IsActive = false;
        _context.SaveChanges();
    }
}