using System.Threading;
using System.Threading.Tasks;

public interface ISupporter
{
    public void Support();
}

public interface IAsyncSupporter
{
    public Task Support(CancellationToken token);
}