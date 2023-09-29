namespace ResursGAP.Models
{
    public class Truck
    {
        public int TruckId { get; set; }
        public string Name { get; set; }

        public double WeightInTons { get; set; }

        public double? LoadedWeightInTons { get; set; } // Масса, которая уже загружена в грузовик
                                                       

    }
}
