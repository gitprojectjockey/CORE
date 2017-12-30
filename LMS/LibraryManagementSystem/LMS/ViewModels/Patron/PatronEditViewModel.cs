using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels.Patron
{
    public class PatronEditViewModel
    {
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string BranchName { get; set; }
        public IEnumerable<string> AvailableBranches { get; set; }
    }
}
