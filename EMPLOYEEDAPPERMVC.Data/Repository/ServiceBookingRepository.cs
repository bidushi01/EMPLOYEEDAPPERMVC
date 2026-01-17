////using EMPLOYEEDAPPERMVC.Data.DataAccess;
////using EMPLOYEEDAPPERMVC.Data.Models.Domain;
////using EMPLOYEEDAPPERMVC.Models.Domain;

////namespace EMPLOYEEDAPPERMVC.Data.Repository
////{
////    public class ServiceBookingRepository : IServiceBookingRepository
////    {
////        private readonly ISqlDataAccess _db;

////        public ServiceBookingRepository(ISqlDataAccess db)
////        {
////            _db = db;
////        }

////        public async Task<IEnumerable<ServiceBooking>> GetAllBookings()
////        {
////            return await _db.GetData<ServiceBooking, dynamic>("sp_GetAllBookings", new { });
////        }

////        public async Task<IEnumerable<AvailableSlot>> GetAppointmentSlots(int serviceId, DateTime bookingDate)
////        {
////            return await _db.GetData<AvailableSlot, dynamic>("sp_GetAppointmentSlots", new { ServiceID = serviceId, BookingDate = bookingDate });
////        }

////        public async Task<IEnumerable<Service>> GetAvailableServices()
////        {
////            return await _db.GetData<Service, dynamic>("sp_GetAvailableServices", new { });
////        }

////        public async Task<IEnumerable<ServiceBooking>> GetServiceBookingSummary(DateTime? startDate, DateTime? endDate, int? serviceId)
////        {
////            return await _db.GetData<ServiceBooking, dynamic>(
////                "sp_GetServiceBookingSummary",
////                new
////                {
////                    StartDate = startDate,
////                    EndDate = endDate,
////                    ServiceId = serviceId
////                });
////        }

////        public async Task<dynamic> CreateBooking(int employeeId, int serviceId,  int customerId, DateTime bookingDate, TimeSpan startTime, TimeSpan endTime)
////        {
////            var result = await _db.GetData<dynamic, dynamic>("sp_CreateBooking_new", new
////            {
////                CustomerID = customerId,
////                EmployeeID = employeeId,
////                ServiceID = serviceId,
////                BookingDate = bookingDate,
////                StartTime = startTime,
////                EndTime = endTime

////            });
////            return result.FirstOrDefault();
////        }

////        public async Task DeleteBooking(int bookingId)
////        {
////            await _db.SaveData<dynamic>("sp_DeleteBooking", new { BookingID = bookingId });
////        }
////    }
////}
//using EMPLOYEEDAPPERMVC.Data.DataAccess;
//using EMPLOYEEDAPPERMVC.Data.Models.Domain;
//using EMPLOYEEDAPPERMVC.Models.Domain;
//using Dapper;
//using System.Linq;
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using System.Data;

//namespace EMPLOYEEDAPPERMVC.Data.Repository
//{
//    public class ServiceBookingRepository : IServiceBookingRepository
//    {
//        private readonly ISqlDataAccess _db;
//        private readonly IConfiguration _config;

//        public ServiceBookingRepository(ISqlDataAccess db, IConfiguration config)
//        {
//            _db = db;
//            _config = config;
//        }

//        public async Task<IEnumerable<ServiceBooking>> GetAllBookings()
//        {
//            return await _db.GetData<ServiceBooking, dynamic>(
//                "sp_GetAllBookings",
//                new { });
//        }

//        public async Task<IEnumerable<AvailableSlot>> GetAppointmentSlots(
//            int serviceId,
//            DateTime bookingDate)
//        {
//            return await _db.GetData<AvailableSlot, dynamic>(
//                "sp_GetAppointmentSlots",
//                new
//                {
//                    ServiceID = serviceId,
//                    BookingDate = bookingDate
//                });
//        }

//        public async Task<IEnumerable<Service>> GetAvailableServices()
//        {
//            return await _db.GetData<Service, dynamic>(
//                "sp_GetAvailableServices",
//                new { });
//        }

//        public async Task<IEnumerable<ServiceBooking>> GetServiceBookingSummary(
//            DateTime? startDate,
//            DateTime? endDate,
//            int? serviceId)
//        {
//            return await _db.GetData<ServiceBooking, dynamic>(
//                "sp_GetServiceBookingSummary",
//                new
//                {
//                    StartDate = startDate,
//                    EndDate = endDate,
//                    ServiceId = serviceId
//                });
//        }


//        public async Task<dynamic> CreateBooking(
//            int employeeId,
//            int serviceId,
//            string customerName,
//            string customerPhone,
//            string customerEmail,
//            DateTime bookingDate,
//            TimeSpan startTime,
//            TimeSpan endTime)
//        {
//            var result = await _db.GetData<dynamic, dynamic>(
//                "sp_CreateBooking_new",
//                new
//                {
//                    EmployeeID = employeeId,
//                    ServiceID = serviceId,
//                    CustomerName = customerName,
//                    CustomerPhone = customerPhone,
//                    CustomerEmail = customerEmail,
//                    BookingDate = bookingDate,
//                    StartTime = startTime,
//                    EndTime = endTime
//                });

//            return result.FirstOrDefault();
//        }

//        public async Task<dynamic> GetDashboardCharts(DateTime? startDate, DateTime? endDate)
//        {
//            using var connection = new SqlConnection(_config.GetConnectionString("conn"));
//            await connection.OpenAsync();

//            using var gridReader = await connection.QueryMultipleAsync(
//                "sp_GetDashboardCharts",
//                new { StartDate = startDate, EndDate = endDate },
//                commandType: CommandType.StoredProcedure);

//            var customers = (await gridReader.ReadAsync<ChartData>()).ToList();
//            var services = (await gridReader.ReadAsync<ChartData>()).ToList();
//            var employees = (await gridReader.ReadAsync<ChartData>()).ToList();
//            var status = (await gridReader.ReadAsync<ChartData>()).ToList();
//            var monthly = (await gridReader.ReadAsync<ChartData>()).ToList();

//            return new
//            {
//                customers = customers,
//                services = services,
//                employees = employees,
//                status = status,
//                monthly = monthly
//            };
//        }

//        public async Task DeleteBooking(int bookingId)
//        {
//            await _db.SaveData<dynamic>(
//                "sp_DeleteBooking",
//                new { BookingID = bookingId });
//        }


//    }
//}

using EMPLOYEEDAPPERMVC.Data.DataAccess;
using EMPLOYEEDAPPERMVC.Data.Models.Domain;
using EMPLOYEEDAPPERMVC.Models.Domain;
using Dapper;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EMPLOYEEDAPPERMVC.Data.Repository
{
    public class ServiceBookingRepository : IServiceBookingRepository
    {
        private readonly ISqlDataAccess _db;
        private readonly IConfiguration _config;

        public ServiceBookingRepository(ISqlDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<IEnumerable<ServiceBooking>> GetAllBookings()
        {
            return await _db.GetData<ServiceBooking, dynamic>(
                "sp_GetAllBookings",
                new { });
        }

        public async Task<IEnumerable<AvailableSlot>> GetAppointmentSlots(
            int serviceId,
            DateTime bookingDate)
        {
            return await _db.GetData<AvailableSlot, dynamic>(
                "sp_GetAppointmentSlots",
                new
                {
                    ServiceID = serviceId,
                    BookingDate = bookingDate
                });
        }

        public async Task<IEnumerable<Service>> GetAvailableServices()
        {
            return await _db.GetData<Service, dynamic>(
                "sp_GetAvailableServices",
                new { });
        }

        public async Task<IEnumerable<ServiceBooking>> GetServiceBookingSummary(
            DateTime? startDate,
            DateTime? endDate,
            int? serviceId)
        {
            return await _db.GetData<ServiceBooking, dynamic>(
                "sp_GetServiceBookingSummary",
                new
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    ServiceId = serviceId
                });
        }


        public async Task<dynamic> CreateBooking(
            int employeeId,
            int serviceId,
            string customerName,
            string customerPhone,
            string customerEmail,
            DateTime bookingDate,
            TimeSpan startTime,
            TimeSpan endTime)
        {
            var result = await _db.GetData<dynamic, dynamic>(
                "sp_CreateBooking_new",
                new
                {
                    EmployeeID = employeeId,
                    ServiceID = serviceId,
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    CustomerEmail = customerEmail,
                    BookingDate = bookingDate,
                    StartTime = startTime,
                    EndTime = endTime
                });

            return result.FirstOrDefault();
        }

        public async Task<dynamic> GetDashboardCharts(DateTime? startDate, DateTime? endDate)
        {
            var param = new
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var customers = await _db.GetData<ChartData, dynamic>(
                "sp_Chart_CustomerBookings", param);

            var services = await _db.GetData<ChartData, dynamic>(
                "sp_Chart_ServiceBookings", param);

            var employees = await _db.GetData<ChartData, dynamic>(
                "sp_Chart_EmployeeBookings", param);

            var status = await _db.GetData<ChartData, dynamic>(
                "sp_Chart_BookingStatus", param);

            var monthly = await _db.GetData<ChartData, dynamic>(
                "sp_Chart_MonthlyBookings", param);

            return new
            {
                customers,
                services,
                employees,
                status,
                monthly
            };
        }


        public async Task DeleteBooking(int bookingId)
        {
            await _db.SaveData<dynamic>(
                "sp_DeleteBooking",
                new { BookingID = bookingId });
        }


    }
}

