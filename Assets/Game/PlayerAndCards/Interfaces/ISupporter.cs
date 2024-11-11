using System.Threading.Tasks;

public interface ISupporter
{
    public void Support();
}

public interface IAsyncSupporter
{
    public Task Support();
}