using EMPLOYEEDAPPERMVC.Data.Models.Domain;

namespace EMPLOYEEDAPPERMVC.Data.Repository
{
    public interface ITimeSelectRepository
    {
        Task<IEnumerable<TimeSelect>> GetAll();
        Task<TimeSelect?> GetById(int id);
        Task<dynamic> Save(TimeSelect timeSelect);
        Task Delete(int id);
        Task<IEnumerable<TimeSelect>> GetTimeSlotsByEmployee(int employeeId);
    }
}

