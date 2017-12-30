using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LMS.DataTransfer.Objects
{
    
    [Identity("PatronDto")]
    public  class PatronDto : Attribute
    {
        public int Id { get; set; }
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string Address { get; set; }
      
        public DateTime DateOfBirth { get; set; }
       
        public string Telephone { get; set; }
        
        public string Gender { get; set; }
       
        public int HomeLibraryBranchId { get; set; }
        
        public LibraryCardDto LibraryCard { get; set; }
       
        public LibraryBranchDto HomeLibraryBranch { get; set; }
    }
}
