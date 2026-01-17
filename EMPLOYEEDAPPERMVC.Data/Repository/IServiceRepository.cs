using System.Collections.Generic;
using System.Threading.Tasks;
using EMPLOYEEDAPPERMVC.Data.Models.Domain;

namespace EMPLOYEEDAPPERMVC.Data.Repository
{
    public interface IServiceRepository
    {

        Task<IEnumerable<Service>> GetAll();
        Task<Service?> GetById(int id);
        Task<int> Save(Service service);
        Task Delete(int id);
    }
}