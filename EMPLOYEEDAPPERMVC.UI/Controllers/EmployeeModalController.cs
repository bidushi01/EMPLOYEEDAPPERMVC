using System.Threading.Tasks;
using EMPLOYEEDAPPERMVC.Data.Models.Domain;
using EMPLOYEEDAPPERMVC.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EMPLOYEEDAPPERMVC.UI.Controllers
{
    public class EmployeeModalController : Controller
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeModalController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var employee = await _repo.GetById(id.Value);
                return PartialView("_EmployeeForm", employee ?? new Employee());
            }
            return PartialView("_EmployeeForm", new Employee());
        }
    }
}
