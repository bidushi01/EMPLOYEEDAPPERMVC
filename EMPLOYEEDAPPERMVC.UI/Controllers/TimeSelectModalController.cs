//using EMPLOYEEDAPPERMVC.Data.Models.Domain;
//using EMPLOYEEDAPPERMVC.Data.Repository;
//using Microsoft.AspNetCore.Mvc;

//namespace EMPLOYEEDAPPERMVC.UI.Controllers
//{
//    public class TimeSelectModalController : Controller
//    {
//        private readonly ITimeSlotRepository _repo;
//        private readonly IEmployeeRepository _employeeRepo;

//        public TimeSelectModalController(ITimeSlotRepository repo, IEmployeeRepository employeeRepo)
//        {
//            _repo = repo;
//            _employeeRepo = employeeRepo;
//        }

//        public async Task<IActionResult> AddOrEdit(int id = 0)
//        {
//            if (id == 0)
//            {
//                return PartialView("_TimeSelectForm", new TimeSlot());
//            }
//            else
//            {
//                var timeSlot = await _repo.GetById(id);
//                return PartialView("_TimeSelectForm", timeSlot);
//            }
//        }
//    }
//}

using EMPLOYEEDAPPERMVC.Data.Models.Domain;
using EMPLOYEEDAPPERMVC.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EMPLOYEEDAPPERMVC.UI.Controllers
{
    public class TimeSelectModalController : Controller
    {
        private readonly ITimeSelectRepository _repo;

        public TimeSelectModalController(ITimeSelectRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return PartialView("_TimeSelectForm", new TimeSelect());
            }
            else
            {
                var timeSelect = await _repo.GetById(id);
                return PartialView("_TimeSelectForm", timeSelect ?? new TimeSelect());
            }
        }
    }
}