//using System.Linq;
//using System.Threading.Tasks;
//using EMPLOYEEDAPPERMVC.Data.Models.Domain;
//using EMPLOYEEDAPPERMVC.Data.Repository;
//using Microsoft.AspNetCore.Mvc;

//namespace EMPLOYEEDAPPERMVC.UI.Controllers
//{
//    public class EmployeeController : Controller
//    {
//        private readonly IEmployeeRepository _repo;
//        private readonly IServiceRepository _serviceRepo;

//        public EmployeeController(IEmployeeRepository repo, IServiceRepository serviceRepo)
//        {
//            _repo = repo;
//            _serviceRepo = serviceRepo;
//        }

//        public async Task<IActionResult> DisplayAll()
//        {
//            var employees = await _repo.GetAll();
//            var services = await _serviceRepo.GetAll();
//            ViewBag.Services = services;
//            return View(employees);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetEmployees()
//        {
//            var employees = await _repo.GetAll();
//            return Json(employees);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetServices()
//        {
//            var services = await _serviceRepo.GetAll();
//            return Json(services);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetEmployeeServiceIds(int employeeId)
//        {
//            var serviceIds = await _repo.GetEmployeeServiceIds(employeeId);
//            return Json(serviceIds);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Save([FromForm] Employee employee, [FromForm] string? ServiceIds)
//        {
//            // Only set ServiceIds if it's explicitly provided (not null/empty)
//            // This preserves existing services when editing employee without changing services
//            if (ServiceIds != null && ServiceIds.Trim() != "")
//            {
//                employee.ServiceIds = ServiceIds;
//            }
//            else
//            {
//                // Set to null to preserve existing services in stored procedure
//                employee.ServiceIds = null;
//            }
//            await _repo.Save(employee);
//            return Ok();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Delete([FromForm] int id)
//        {
//            await _repo.Delete(id);
//            return Ok();
//        }
//    }
//}

using EMPLOYEEDAPPERMVC.Data.Models.Domain;
using EMPLOYEEDAPPERMVC.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EMPLOYEEDAPPERMVC.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repo;
        private readonly IServiceRepository _serviceRepo;

        public EmployeeController(IEmployeeRepository repo, IServiceRepository serviceRepo)
        {
            _repo = repo;
            _serviceRepo = serviceRepo;
        }

        public IActionResult DisplayAll()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Json(await _repo.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            return Json(await _serviceRepo.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeServiceIds(int employeeId)
        {
            return Json(await _repo.GetEmployeeServiceIds(employeeId));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromForm] Employee employee)
        {
            await _repo.Save(employee);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await _repo.Delete(id);
            return Ok();
        }
    }
}
