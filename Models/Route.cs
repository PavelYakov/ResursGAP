using System.ComponentModel.DataAnnotations.Schema;

namespace ResursGAP.Models
{
    public class Route
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }
        [ForeignKey("Truck")]
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
    }
}
