//using System.Collections.Generic;
//using EMPLOYEEDAPPERMVC.Data.Repository;
//using EMPLOYEEDAPPERMVC.Models.Domain;
//using Microsoft.AspNetCore.Mvc;

//namespace EMPLOYEEDAPPERMVC.UI.Controllers
//{
//    public class ServiceBookingController : Controller
//    {
//        private readonly IServiceBookingRepository _bookingRepo;

//        public ServiceBookingController(IServiceBookingRepository bookingRepo)
//        {
//            _bookingRepo = bookingRepo;
//        }

//        public IActionResult DisplayAll()
//        {
//            return View();
//        }

//        public IActionResult Summary()
//        {
//            return View();
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetBookings()
//        {
//            return Json(await _bookingRepo.GetAllBookings());
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAvailableServices()
//        {
//            return Json(await _bookingRepo.GetAvailableServices());
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetServiceBookingSummary(DateTime? startDate,  DateTime? endDate, int? serviceId)
//        {
//            var data = await _bookingRepo.GetServiceBookingSummary(startDate, endDate, serviceId);
//            return Json(data);
//        }

//        //[HttpPost]
//        //public async Task<IActionResult> GetAvailableSlots(int serviceId, string bookingDate)
//        //{
//        //    var date = DateTime.Parse(bookingDate);
//        //    var slots = await _bookingRepo.GetAppointmentSlots(serviceId, date);
//        //    return Json(slots ?? new List<AvailableSlot>());
//        //}

//        [HttpPost]
//        public async Task<IActionResult> GetAvailableSlots(int serviceId, DateTime bookingDate)
//        {
//            var slots = await _bookingRepo.GetAppointmentSlots(serviceId, bookingDate);
//            return Json(slots ?? new List<AvailableSlot>());
//        }


//        [HttpPost]
//        public async Task<IActionResult> Save(int employeeId, int serviceId, string bookingDate, string startTime, string endTime, int customerId)
//        {
//            var result = await _bookingRepo.CreateBooking(employeeId, serviceId,customerId, DateTime.Parse(bookingDate), TimeSpan.Parse(startTime), TimeSpan.Parse(endTime));
//            var hasError = result?.HasError ?? 1;
//            var message = result?.Message ?? "Error occurred";

//            if (hasError == 0)
//            {
//                return Json(new { success = true, message = message });
//            }
//            else
//            {
//                return Json(new { success = false, message = message });
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _bookingRepo.DeleteBooking(id);
//            return Json(new { success = true });
//        }
//    }
//}using EMPLOYEEDAPPERMVC.Data.Repository;
using EMPLOYEEDAPPERMVC.Data.Repository;
using EMPLOYEEDAPPERMVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace EMPLOYEEDAPPERMVC.UI.Controllers
{
    public class ServiceBookingController : Controller
    {
        private readonly IServiceBookingRepository _bookingRepo;

        public ServiceBookingController(IServiceBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public IActionResult DisplayAll()
        {
            return View();
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            return Json(await _bookingRepo.GetAllBookings());
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableServices()
        {
            return Json(await _bookingRepo.GetAvailableServices());
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceBookingSummary(
            DateTime? startDate,
            DateTime? endDate,
            int? serviceId)
        {
            var data = await _bookingRepo
                .GetServiceBookingSummary(startDate, endDate, serviceId);

            return Json(data);
        }



        [HttpPost]
        public async Task<IActionResult> GetAvailableSlots(
            int serviceId,
            DateTime bookingDate)
        {
            var slots = await _bookingRepo
                .GetAppointmentSlots(serviceId, bookingDate);

            return Json(slots ?? new List<AvailableSlot>());
        }

       
        [HttpPost]
        public async Task<IActionResult> Save(
            int employeeId,
            int serviceId,
            string bookingDate,
            string startTime,
            string endTime,
            string customerName,
            string customerPhone,
            string customerEmail)
        {
            var result = await _bookingRepo.CreateBooking(
                employeeId,
                serviceId,
                customerName,
                customerPhone,
                customerEmail,
                DateTime.Parse(bookingDate),
                TimeSpan.Parse(startTime),
                TimeSpan.Parse(endTime));

            if (result?.HasError == 0)
            {
                return Json(new
                {
                    success = true,
                    message = result.Message
                });
            }

            return Json(new
            {
                success = false,
                message = result?.Message ?? "Booking failed"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookingRepo.DeleteBooking(id);
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardCharts(DateTime? startDate, DateTime? endDate)
        {
            var charts = await _bookingRepo.GetDashboardCharts(startDate, endDate);
            return Json(charts);
        }
    }
}

