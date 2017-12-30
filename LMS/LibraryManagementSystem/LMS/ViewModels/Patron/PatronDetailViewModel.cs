using LibraryData.Models;
using LibraryServices.ServiceModels;
using System;
using System.Collections.Generic;

namespace LMS.ViewModels.Patron
{
    public class PatronDetailViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
        public int LibraryCardId { get; set; }
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string HomeLibrary { get; set; }
        public decimal OverdueFees { get; set; }
        public IEnumerable<CheckoutModel> AssetsCheckedOut { get; set; }
        public IEnumerable<CheckoutHistoryModel> CheckoutHistory { get; set; }
        public IEnumerable<HoldModel> Holds { get; set; }
    }
}
