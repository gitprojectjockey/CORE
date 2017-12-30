namespace LibraryServices.ServiceModels
{
    public class BanchHoursModel
    {
        public int Id { get; set; }
        public LibraryBranchModel Branch { get; set; }
        public int DayOfWeek { get; set; }
        public int OpenTime { get; set; }
        public int CloseTime { get; set; }
    }
}
