using EWN.Data.Repo.Core;

namespace PostalService.Data.Entities
{
    public class State
    {
        public int StateId { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public bool IsPrimaryState { get; set; }
        public string SSCode { get; set; }
        public string USRegion { get; set; }
       
    }
}
