using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;

namespace MicroService_Gateway.DTO
{
    
    public class Agriculture { 
        public int? Nitrogen { get; set; }
        public int? Phosphorus { get; set; }
        public int? Potassium { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Ph { get; set; }
        public double? Rainfall { get; set; }
        public string? CropType { get; set; }
    }

    public class AgricultureClassMap : ClassMap<Agriculture> { 
        public AgricultureClassMap() { 
            Map(agr => agr.Nitrogen).Name("N");
            Map(agr => agr.Phosphorus).Name("P");
            Map(agr => agr.Potassium).Name("K");
            Map(agr => agr.Temperature).Name("temperature");
            Map(agr => agr.Humidity).Name("humidity");
            Map(agr => agr.Ph).Name("ph");
            Map(agr => agr.Rainfall).Name("rainfall");
            Map(agr => agr.CropType).Name("label");
        }
    }
}