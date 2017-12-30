namespace EWNData.Dto
{
    public class Company : ICompany
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string State { get; set; }
    }
}
