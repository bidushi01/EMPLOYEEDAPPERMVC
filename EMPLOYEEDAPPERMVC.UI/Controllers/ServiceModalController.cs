using System.Threading.Tasks;
using EMPLOYEEDAPPERMVC.Data.Models.Domain;
using EMPLOYEEDAPPERMVC.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EMPLOYEEDAPPERMVC.UI.Controllers
{
    public class ServiceModalController : Controller
    {
        private readonly IServiceRepository _repo;

        public ServiceModalController(IServiceRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var service = await _repo.GetById(id.Value);
                return PartialView("_ServiceForm", service ?? new Service());
            }
            return PartialView("_ServiceForm", new Service());
        }
    }
}
