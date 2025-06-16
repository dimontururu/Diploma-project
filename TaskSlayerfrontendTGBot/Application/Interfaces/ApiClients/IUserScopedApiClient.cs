namespace Application.Interfaces.ApiClients
{
    public interface IUserScopedApiClient
    {
        Task<T> ExecuteAsUserAsync<T>(Func<ITaskSlayerApiClient, Task<T>> action);
        Task ExecuteAsUserAsync(Func<ITaskSlayerApiClient, Task> action);
    }
}
