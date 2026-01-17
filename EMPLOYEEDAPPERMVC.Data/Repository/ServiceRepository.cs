namespace EMPLOYEEDAPPERMVC.Data.Repository;

using EMPLOYEEDAPPERMVC.Data.DataAccess;
using EMPLOYEEDAPPERMVC.Data.Models.Domain;

public class ServiceRepository : IServiceRepository
{
    private readonly ISqlDataAccess _db;

    public ServiceRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Service>> GetAll()
    {
        return await _db.GetData<Service, dynamic>("sp_GetAllServices", new { });
    }

    public async Task<Service?> GetById(int id)
    {
        var result = await _db.GetData<Service, dynamic>("sp_GetServiceById", new { Id = id });
        return result.FirstOrDefault();
    }

    public async Task<int> Save(Service service)
    {
        return await _db.SaveDataAndGet<int, dynamic>("sp_SaveService", new
        {
            Id = service.Id,
            Name = service.Name,
            DurationMinute = service.DurationMinute,
            Charge = service.Charge
        });
    }

    public async Task Delete(int id)
    {
        await _db.SaveData<dynamic>("sp_DeleteService", new { Id = id });
    }
}