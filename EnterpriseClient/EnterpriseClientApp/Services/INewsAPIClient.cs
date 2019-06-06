using EnterpriseClientApp.Models;
using System.Threading.Tasks;

namespace EnterpriseClientApp.Services
{
    public interface INewsAPIClient
    {
        Task<Articles[]> GetValuesforNews();
        Task SetTokenforNews();
    }
}