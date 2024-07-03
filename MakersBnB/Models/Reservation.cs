namespace MakersBnB.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int SpaceId { get; set; }
        public Space Space { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}
