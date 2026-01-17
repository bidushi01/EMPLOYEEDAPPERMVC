using System.Collections.Generic;
using System;
namespace EMPLOYEEDAPPERMVC.Data.Models.Domain;

public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
}