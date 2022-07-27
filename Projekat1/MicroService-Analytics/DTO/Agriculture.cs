namespace MicroService_Analytics.DTO 
{
    public class Agriculture 
    {
        public int Nitrogen { get; set; }
        public int Phosphorus { get; set; }
        public int Potassium { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Ph { get; set; }
        public double Rainfall { get; set; }
        public string CropType { get; set; }
    }
}