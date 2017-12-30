using System.Collections.Generic;

namespace LMS.ViewModels.Patron
{
    public class PatronIndexViewModel
    {
        private readonly IEnumerable<PatronDetailViewModel> _patrons;
        public PatronIndexViewModel(IEnumerable<PatronDetailViewModel> patrons)
        {
            _patrons = patrons;
        }

        public IEnumerable<PatronDetailViewModel> Patrons
        {
            get { return _patrons; }
        }
    }
}
