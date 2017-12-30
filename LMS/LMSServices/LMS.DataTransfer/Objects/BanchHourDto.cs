namespace LMS.DataTransfer.Objects
{
    [Identity("BranchHourDto")]
    public class BranchHourDto
    {
        public int Id { get; set; }
        public LibraryBranchDto Branch { get; set; }
        public int DayOfWeek { get; set; }
        public int OpenTime { get; set; }
        public int CloseTime { get; set; }
    }
}
