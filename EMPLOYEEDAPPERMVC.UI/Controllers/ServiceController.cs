//using System.Linq;
//using System.Threading.Tasks;
//using EMPLOYEEDAPPERMVC.Data.Models.Domain;
//using EMPLOYEEDAPPERMVC.Data.Repository;
//using Microsoft.AspNetCore.Mvc;


//namespace EMPLOYEEDAPPERMVC.UI.Controllers
//{
//    public class ServiceController : Controller
//    {
//        private readonly IServiceRepository _repo;
//        private readonly IEmployeeRepository _employeeRepo;

//        public ServiceController(IServiceRepository Repo, IEmployeeRepository employeeRepo)
//        {
//            _repo = Repo;
//            _employeeRepo = employeeRepo;
//        }

//        public async Task<IActionResult> DisplayAll()
//        {

//            var services = await _repo.GetAll();
//            return View(services);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetServices()
//        {
//            var services = await _repo.GetAll();
//            return Json(services);
//        }




//        [HttpPost]
//        public async Task<IActionResult> Save([FromForm]  Service service)
//        {
//             await _repo.Save(service);

//            return Ok();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _repo.Delete(id);
//            return Ok();
//        }

//        public async Task<IActionResult> AssignEmployee()
//        {
//            return View();
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetEmployeesWithServices()
//        {
//            var employees = await _employeeRepo.GetEmployeesWithServices();
//            return Json(employees);
//        }

//        [HttpPost]
//        public async Task<IActionResult> SaveEmployeeService(int employeeId, int serviceId)
//        {
//            await _employeeRepo.SaveEmployeeService(employeeId, serviceId);
//            return Ok();
//        }

//        [HttpPost]
//        public async Task<IActionResult> DeleteEmployeeService(int employeeId, int serviceId)
//        {
//            await _employeeRepo.DeleteEmployeeService(employeeId, serviceId);
//            return Ok();
//        }
//    }
//}

using EMPLOYEEDAPPERMVC.Data.Models.Domain;
using EMPLOYEEDAPPERMVC.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EMPLOYEEDAPPERMVC.UI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _repo;
        private readonly IEmployeeRepository _employeeRepo;

        public ServiceController(IServiceRepository repo, IEmployeeRepository employeeRepo)
        {
            _repo = repo;
            _employeeRepo = employeeRepo;
        }

        public IActionResult DisplayAll()
        {
            return View();
        }

        public IActionResult AssignEmployee()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            return Json(await _repo.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesWithServices()
        {
            return Json(await _employeeRepo.GetEmployeesWithServices());
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromForm] Service service)
        {
            await _repo.Save(service);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployeeService(int employeeId, int serviceId)
        {
            await _employeeRepo.SaveEmployeeService(employeeId, serviceId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployeeService(int employeeId, int serviceId)
        {
            await _employeeRepo.DeleteEmployeeService(employeeId, serviceId);
            return Ok();
        }
    }
}









