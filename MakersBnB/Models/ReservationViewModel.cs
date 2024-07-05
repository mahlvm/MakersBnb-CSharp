using System.Collections.Generic;

namespace MakersBnB.Models
{
    public class ReservationViewModel
    {
        public List<Space> Spaces { get; set; } = new List<Space>();
        public Reservation Reservation { get; set; } = new Reservation();
    }
}
