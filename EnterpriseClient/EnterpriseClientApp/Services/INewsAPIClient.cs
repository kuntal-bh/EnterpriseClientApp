using System.Threading.Tasks;

namespace EnterpriseClientApp.Services
{
    public interface INewsAPIClient
    {
        Task<string[]> GetValuesforNews();
        Task SetTokenforNews();
    }
}