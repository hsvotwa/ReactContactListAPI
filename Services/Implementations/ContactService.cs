using Microsoft.EntityFrameworkCore;
using ReactContactListApi.EntityData;
using ReactContactListApi.Models;
using ReactContactListApi.Services.Interface;
using ReactContactListApi.Utilities;

namespace ReactContactListApi.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly AppDBContext _context;
        private readonly ILogger<ContactService> _logger;

        public ContactService(AppDBContext context, ILogger<ContactService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GlobalResponse<int>> SaveContact(Contact record)
        {
            try
            {
                await _context.AddAsync(record);
                var feedback = await _context.SaveChangesAsync() > 0 ? record.ID : 0;

                return new GlobalResponse<int>
                {
                    Feedback = feedback,
                    Description = feedback == 0 ? "Saving failed" : "Successfully saved"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GlobalResponse<int> { Feedback = 0, Description = ex.Message };
            }
        }

        public async Task<GlobalResponse<bool>> UpdateContact(Contact record)
        {
            try
            {
                if (!await ContactExists(record.ID))
                {
                    return new GlobalResponse<bool> { Feedback = false, Description = "Record not found." };
                }
                _context.Contacts.Update(record);
                var feedback = await _context.SaveChangesAsync() > 0;

                return new GlobalResponse<bool>
                {
                    Feedback = feedback,
                    Description = feedback ? "Update failed" : "Successfully upated"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GlobalResponse<bool> { Feedback = false, Description = ex.Message };
            }
        }
        public async Task<GlobalResponse<bool>> DeleteContact(int id)
        {
            try
            {
                if (!await ContactExists(id))
                {
                    return new GlobalResponse<bool> { Feedback = false, Description = "Record not found." };
                }

                Contact contactToDelete = new Contact() { ID = id };
                _context.Remove(contactToDelete);
                var feedback = await _context.SaveChangesAsync() > 0;

                return new GlobalResponse<bool>
                {
                    Feedback = feedback,
                    Description = feedback ? "Deletion failed" : "Successfully deleted"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GlobalResponse<bool> { Feedback = false, Description = ex.Message };
            }
        }

        public async Task<Contact> GetContact(int id)
        {
            try
            {
                return await _context.Contacts.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }

        public async Task<List<Contact>> GetContacts()
        {
            try
            {
                return await _context.Contacts.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return new();
        }

        private async Task<bool> ContactExists(int id)
        {
            try
            {
                return await _context.Contacts.AnyAsync(e => e.ID == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return false;
        }
    }
}
