using System.Threading.Tasks;

public interface ISupportBh
{
    public void Support();
}

public interface IAsyncSupportBh
{
    public Task Support();
}