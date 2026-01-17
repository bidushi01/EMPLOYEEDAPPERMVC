using Microsoft.AspNetCore.Mvc;

namespace EMPLOYEEDAPPERMVC.UI.Controllers
{
    public class ServiceBookingModalController : Controller
    {
        [HttpGet]
        public IActionResult AddOrEdit(int employeeId, int serviceId, string serviceName, string bookingDate, string timeSlot, string startTime, string endTime)
        {
            ViewBag.EmployeeId = employeeId;
            ViewBag.ServiceId = serviceId;
            ViewBag.ServiceName = serviceName;
            ViewBag.BookingDate = bookingDate;
            ViewBag.TimeSlot = timeSlot;
            ViewBag.StartTime = startTime;
            ViewBag.EndTime = endTime;
            return PartialView("_ServiceBookingForm");
        }
    }
}




