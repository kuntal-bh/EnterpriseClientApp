using System.Threading.Tasks;

namespace EnterpriseClientApp.Services
{
    public interface IReceipeAPIClient
    {
        Task<string[]> GetValuesforNews();
        Task SetTokenforNews();
    }
}