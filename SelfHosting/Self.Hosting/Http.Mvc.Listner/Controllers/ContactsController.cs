using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Http.Mvc.Listner.Models;
using Http.Mvc.Listner.Services ;

namespace Http.Mvc.Listner.Controllers
{
    public class ContactsController : Controller
    {
        private IHostingEnvironment _hostingEnviornment;

        public ContactsController(IHostingEnvironment hostingEnviornment)
        {
            _hostingEnviornment = hostingEnviornment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var jsonFilePath = Path.Combine(_hostingEnviornment.WebRootPath, @"json\Contacts.json");

            List<Contact> contacts;
            using (StreamReader stream = new StreamReader(jsonFilePath))
            {
                string json = stream.ReadToEnd();
                contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            }

            ContactDataValidatorService cdvs = new ContactDataValidatorService(contacts);
            var invalidContacts = cdvs.ValidateJsonValues();
            var invalidContactsJson = JsonConvert.SerializeObject(invalidContacts,Formatting.Indented);

            return Content(invalidContactsJson);
        }
    }
}
