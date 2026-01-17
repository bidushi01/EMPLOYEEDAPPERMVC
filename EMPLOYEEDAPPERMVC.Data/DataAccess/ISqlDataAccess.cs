
namespace EMPLOYEEDAPPERMVC.Data.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string spName, P parameter, string connectionId = "conn");

        Task SaveData<T>(string spName, T parameter, string connectionId = "conn");
        Task<T> SaveDataAndGet<T, P>(string spName, P parameter, string connectionId = "conn");
    }

}