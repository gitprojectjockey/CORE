using System;

namespace LibraryServices.ServiceModels
{
    public  class PatronModel
    {
        public int Id { get; set; }
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string Address { get; set; }
      
        public DateTime DateOfBirth { get; set; }
       
        public string Telephone { get; set; }
        
        public string Gender { get; set; }
       
        public int HomeLibraryBranchId { get; set; }
        
        public LibraryCardModel LibraryCard { get; set; }
       
        public LibraryBranchModel HomeLibraryBranch { get; set; }
    }
}
