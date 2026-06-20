using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities;
public class Speciality : BaseEntity
{ 
    public required string Name { get; set; }
    public required string Description { get; set; }
}

