using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data.Interfaces;

public interface IPersistence
{
    List<Doctor> GetDoctors();
    Doctor GetDoctorById(Guid id);
    void AddDoctor(Doctor doctor);
    List<Speciality> GetSpecialities();
    Speciality GetSpecialityById(Guid id);
    void DeactivateDoctor(Doctor doctor);
}
