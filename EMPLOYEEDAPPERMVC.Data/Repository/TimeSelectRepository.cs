using EMPLOYEEDAPPERMVC.Data.DataAccess;
using EMPLOYEEDAPPERMVC.Data.Models.Domain;

namespace EMPLOYEEDAPPERMVC.Data.Repository;

public class TimeSelectRepository : ITimeSelectRepository
{
    private readonly ISqlDataAccess _db;

    public TimeSelectRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<TimeSelect>> GetAll()
    {
        return await _db.GetData<TimeSelect, dynamic>("sp_GetAllTimeSlots", new { });
    }

    public async Task<TimeSelect?> GetById(int id)
    {
        var result = await _db.GetData<TimeSelect, dynamic>("sp_GetTimeSlotById", new { Id = id });
        return result.FirstOrDefault();
    }

    public async Task<dynamic> Save(TimeSelect timeSelect)
    {
        var result = await _db.GetData<dynamic, dynamic>("sp_SaveTimeSlot", new
        {
            Id = timeSelect.Id,
            EmployeeId = timeSelect.EmployeeId,
            StartTime = timeSelect.StartTime,
            EndTime = timeSelect.EndTime
        });
        return result.FirstOrDefault();
    }

    public async Task Delete(int id)
    {
        await _db.SaveData<dynamic>("sp_DeleteTimeSlot", new { Id = id });
    }

    public async Task<IEnumerable<TimeSelect>> GetTimeSlotsByEmployee(int employeeId)
    {
        var all = await GetAll();
        return all.Where(x => x.EmployeeId == employeeId);
    }
}