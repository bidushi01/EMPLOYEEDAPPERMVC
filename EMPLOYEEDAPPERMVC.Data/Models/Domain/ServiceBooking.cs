namespace EMPLOYEEDAPPERMVC.Models.Domain
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ServiceBooking
    {
        public int BookingID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }  
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Status { get; set; }
    }

    public class AvailableSlot
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ServiceId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Booked { get; set; }
    }

    public class BookedSlot
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int EmployeeID { get; set; }
    }
}
