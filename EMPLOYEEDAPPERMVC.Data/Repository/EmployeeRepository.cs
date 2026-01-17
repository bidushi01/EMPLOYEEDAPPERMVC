//namespace EMPLOYEEDAPPERMVC.Data.Repository;

//using EMPLOYEEDAPPERMVC.Data.DataAccess;
//using EMPLOYEEDAPPERMVC.Data.Models.Domain;

//public class EmployeeRepository : IEmployeeRepository
//{
//    private readonly ISqlDataAccess _db;

//    public EmployeeRepository(ISqlDataAccess db)
//    {
//        _db = db;
//    }

//    public async Task<IEnumerable<Employee>> GetAll()
//    {
//        return await _db.GetData<Employee, dynamic>("sp_GetAllEmployees", new { });
//    }

//    public async Task<Employee?> GetById(int id)
//    {
//        var result = await _db.GetData<Employee, dynamic>("sp_GetEmployeeById", new { Id = id });
//        return result.FirstOrDefault();
//    }

//    public async Task<int> Save(Employee employee)
//    {
//        return await _db.SaveDataAndGet<int, dynamic>("sp_SaveEmployee", new
//        {
//            Id = employee.Id,
//            Name = employee.Name,
//            Email = employee.Email,
//            Phone = employee.Phone,
//            Address = employee.Address,
//            ServiceIds = employee.ServiceIds
//        });
//    }

//    public async Task Delete(int id)
//    {
//        await _db.SaveData<dynamic>("sp_DeleteEmployee", new { Id = id });
//    }

//    public async Task<IEnumerable<int>> GetEmployeeServiceIds(int employeeId)
//    {
//        var result = await _db.GetData<dynamic, dynamic>("sp_GetEmployeeServiceIds", new { EmployeeId = employeeId });
//        return result.Select(x => (int)x.id);
//    }

//    public async Task<IEnumerable<EmployeeWithService>> GetEmployeesWithServices()
//    {
//        return await _db.GetData<EmployeeWithService, dynamic>("sp_GetEmployeesWithServices", new { });
//    }

//    public async Task SaveEmployeeService(int employeeId, int serviceId)
//    {
//        await _db.SaveData<dynamic>("sp_SaveEmployeeService", new { EmployeeId = employeeId, ServiceId = serviceId });
//    }

//    public async Task DeleteEmployeeService(int employeeId, int serviceId)
//    {
//        await _db.SaveData<dynamic>("sp_DeleteEmployeeService", new { EmployeeId = employeeId, ServiceId = serviceId });
//    }
//}

using EMPLOYEEDAPPERMVC.Data.DataAccess;
using EMPLOYEEDAPPERMVC.Data.Models.Domain;

namespace EMPLOYEEDAPPERMVC.Data.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ISqlDataAccess _db;

    public EmployeeRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _db.GetData<Employee, dynamic>("sp_GetAllEmployees", new { });
    }

    public async Task<Employee?> GetById(int id)
    {
        var result = await _db.GetData<Employee, dynamic>("sp_GetEmployeeById", new { Id = id });
        return result.FirstOrDefault();
    }

    public async Task<int> Save(Employee employee)
    {
        return await _db.SaveDataAndGet<int, dynamic>("sp_SaveEmployee", new
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Phone = employee.Phone,
            Address = employee.Address
        });
    }

    public async Task Delete(int id)
    {
        await _db.SaveData<dynamic>("sp_DeleteEmployee", new { Id = id });
    }

    public async Task<IEnumerable<int>> GetEmployeeServiceIds(int employeeId)
    {
        var result = await _db.GetData<dynamic, dynamic>("sp_GetEmployeeServiceIds", new { EmployeeId = employeeId });
        return result.Select(x => (int)x.id);
    }

    public async Task<IEnumerable<EmployeeWithService>> GetEmployeesWithServices()
    {
        return await _db.GetData<EmployeeWithService, dynamic>("sp_GetEmployeesWithServices", new { });
    }

    public async Task SaveEmployeeService(int employeeId, int serviceId)
    {
        await _db.SaveData<dynamic>("sp_SaveEmployeeService", new { EmployeeId = employeeId, ServiceId = serviceId });
    }

    public async Task DeleteEmployeeService(int employeeId, int serviceId)
    {
        await _db.SaveData<dynamic>("sp_DeleteEmployeeService", new { EmployeeId = employeeId, ServiceId = serviceId });
    }

    public async Task<IEnumerable<Employee>> GetEmployeesWithoutTimeSlot()
    {
        return await _db.GetData<Employee, dynamic>("sp_GetEmployeesWithoutTimeSlot", new { });
    }
}