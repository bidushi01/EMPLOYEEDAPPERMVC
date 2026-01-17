using EMPLOYEEDAPPERMVC.Data.Models.Domain;
using EMPLOYEEDAPPERMVC.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EMPLOYEEDAPPERMVC.UI.Controllers
{
    public class TimeSelectController : Controller
    {
        private readonly ITimeSelectRepository _repo;
        private readonly IEmployeeRepository _employeeRepo;

        public TimeSelectController(ITimeSelectRepository repo, IEmployeeRepository employeeRepo)
        {
            _repo = repo;
            _employeeRepo = employeeRepo;
        }

        public IActionResult DisplayAll()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeSlots()
        {
            return Json(await _repo.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Json(await _employeeRepo.GetEmployeesWithoutTimeSlot());
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromForm] TimeSelect timeSelect)
        {
            var result = await _repo.Save(timeSelect);
            var timeSlotId = result?.TimeSlotId ?? 0;
            var message = result?.Message ?? "";

            if (timeSlotId > 0)
            {
                return Json(new { success = true, message = message });
            }
            else
            {
                return Json(new { success = false, message = message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await _repo.Delete(id);
            return Ok();
        }
    }
}