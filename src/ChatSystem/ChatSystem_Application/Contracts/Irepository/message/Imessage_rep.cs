using ChatSystem_Application.Contracts.IGenericRepository;
using ChatSystem_Domain.Model.message;

namespace ChatSystem_Application.Contracts.Irepository.message
{
    public interface Imessage_rep : IGenericRepository<Message_E>
    {
        Task<List<Message_E>> GetByOrder(int sender , int reciver , int order);
    }
}
