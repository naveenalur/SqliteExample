using System.Threading.Tasks;

namespace SqlLiteSimple.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}