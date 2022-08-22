using Microsoft.AspNetCore.Mvc;
using ReactContactListApi.Models;
using ReactContactListApi.Services.Interface;
using ReactContactListApi.Utilities;

namespace ReactContactListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<ActionResult<GlobalResponse<int>>> SaveContact(Contact record)
        {
            return ModelState.IsValid switch
            {
                true => await _contactService.SaveContact(record),
                _ => new  GlobalResponse<int> { Feedback = 0, Description = "All fields are required"}
            };
        }

        [HttpPut]
        public async Task<ActionResult<GlobalResponse<bool>>> UpdateContact(Contact record)
        {
            return ModelState.IsValid switch
            {
                true => await _contactService.UpdateContact(record),
                _ => new GlobalResponse<bool> { Feedback = false, Description = "All fields are required" }
            };
        }

        [HttpDelete]
        public async Task<ActionResult<GlobalResponse<bool>>> DeleteContact(int id) => await _contactService.DeleteContact(id);

        [HttpGet]
        public async Task<ActionResult<Contact>> GetContact(int id) => await _contactService.GetContact(id);

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetContacts() => await _contactService.GetContacts();

    }
}