namespace EMPLOYEEDAPPERMVC.Data.Models.Domain;

public class TimeSelect
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? EmployeeName { get; set; }
}
