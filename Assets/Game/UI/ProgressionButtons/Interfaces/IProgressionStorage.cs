public interface IProgressionStorage
{
    int GetInt(string key, int defaultValue);
    void SetInt(string key, int value);
    void Save();
}