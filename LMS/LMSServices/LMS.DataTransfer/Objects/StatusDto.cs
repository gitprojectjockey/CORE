namespace LMS.DataTransfer.Objects
{
    [Identity("StatusDto")]
    public class StatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
