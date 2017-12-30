using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.ResourceModels
{
    public class BranchDetailResourceModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string BranchName { get; set; }
        public string BranchOpenedDate { get; set; }
        public string Telephone { get; set; }
        public bool IsOpen { get; set; }
        public string Description { get; set; }
        public int NumberOfPatrons { get; set; }
        public int NumberOfAssets { get; set; }
        public decimal TotalAssetValue { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<BranchHourDto> HoursOpen { get; set; }
        public IEnumerable<string> HumanizedBranchHours { get; set; }
    }
}
