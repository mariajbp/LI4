using System;
using System.Threading.Tasks;

namespace TicketNow.Services
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
