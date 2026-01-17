using EMPLOYEEDAPPERMVC.Data.Models.Domain;
using EMPLOYEEDAPPERMVC.Models.Domain;

namespace EMPLOYEEDAPPERMVC.Data.Repository
{
    public interface IServiceBookingRepository
    {
        Task<IEnumerable<ServiceBooking>> GetAllBookings();
        Task<IEnumerable<AvailableSlot>> GetAppointmentSlots(int serviceId, DateTime bookingDate);
        Task<IEnumerable<Service>> GetAvailableServices();
        Task<IEnumerable<ServiceBooking>> GetServiceBookingSummary(DateTime? startDate, DateTime? endDate, int? serviceId);

        Task<dynamic> CreateBooking(int employeeId, int serviceId, string customerName, string customerPhone, string customerEmail, DateTime bookingDate, TimeSpan startTime, TimeSpan endTime);
        Task<dynamic> GetDashboardCharts(DateTime? startDate, DateTime? endDate);
        Task DeleteBooking(int bookingId);
    }
}
