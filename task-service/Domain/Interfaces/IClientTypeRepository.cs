using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces
{ 
    public interface IClientTypeRepository
    {
        Task<ClientType?> GetByTypeAsync(string typeId);
    }
}
