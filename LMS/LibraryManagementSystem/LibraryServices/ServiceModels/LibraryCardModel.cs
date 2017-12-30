using System;
using System.Collections.Generic;

namespace LibraryServices.ServiceModels
{
    public class LibraryCardModel
    {
        public int Id { get; set; }
        public decimal Fees { get; set; }
        public DateTime Created { get; set; }
        public virtual IEnumerable<CheckoutModel> Checkouts { get; set; }
    }
}
