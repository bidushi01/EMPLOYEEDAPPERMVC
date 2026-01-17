//using EMPLOYEEDAPPERMVC.Data.Models.Domain;

//namespace EMPLOYEEDAPPERMVC.Data.Repository
//{
//    public interface IEmployeeRepository
//    {
//        Task<IEnumerable<Employee>> GetAll();
//        Task<Employee?> GetById(int id);
//        Task<int> Save(Employee employee);
//        Task Delete(int id);
//        Task<IEnumerable<int>> GetEmployeeServiceIds(int employeeId);
//        Task<IEnumerable<EmployeeWithService>> GetEmployeesWithServices();
//        Task SaveEmployeeService(int employeeId, int serviceId);
//        Task DeleteEmployeeService(int employeeId, int serviceId);
//    }
//}

using EMPLOYEEDAPPERMVC.Data.Models.Domain;

namespace EMPLOYEEDAPPERMVC.Data.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee?> GetById(int id);
        Task<int> Save(Employee employee);
        Task Delete(int id);
        Task<IEnumerable<int>> GetEmployeeServiceIds(int employeeId);
        Task<IEnumerable<EmployeeWithService>> GetEmployeesWithServices();
        Task SaveEmployeeService(int employeeId, int serviceId);
        Task DeleteEmployeeService(int employeeId, int serviceId);
        Task<IEnumerable<Employee>> GetEmployeesWithoutTimeSlot();
    }
}