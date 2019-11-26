using System.Threading;
using System.Threading.Tasks;

namespace ICNCLib
{
    public interface IMachineComms
    {
        Task<IMachinePosition> GetPositionAsync(CancellationToken ct);
        Task<IMachinePosition> WaitforInPositionAsync(CancellationToken ct);
        Task WaitforStartCollectionAsync(CancellationToken ct);
        Task SendProgramAsync(string sourceFileName);
        Task<bool> ConnectAsync();
        Task<bool> DisconnectAsync();
    }

}
