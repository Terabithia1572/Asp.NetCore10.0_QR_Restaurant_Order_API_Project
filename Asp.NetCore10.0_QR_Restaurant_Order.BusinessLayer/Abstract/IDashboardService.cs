
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DashboardDTO;
using System.Threading.Tasks;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract
{
    public interface IDashboardService
    {
        Task<ResultDashboardSummaryDTO> GetDashboardSummaryAsync();
    }
}
