using ReactContactListApi.Models;
using ReactContactListApi.Utilities;

namespace ReactContactListApi.Services.Interface
{
    public interface IContactService
    {
        Task<GlobalResponse<int>> SaveContact(Contact record);
        Task<GlobalResponse<bool>> UpdateContact(Contact record);
        Task<GlobalResponse<bool>> DeleteContact(int id);
        Task<Contact> GetContact(int id);
        Task<List<Contact>> GetContacts();
    }
}