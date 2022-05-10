// using System.Globalization;
// using System.Text;
// using CsvHelper;
// using CsvHelper.Configuration;
// using MicroService_Gateway.Models;

// namespace MicroService_Gateway.CSV
// { 

//     public class MyCsvReader 
//     { 
//         private string? fileName;
//         private CsvConfiguration csvConfiguration;

//         public MyCsvReader(string filename) 
//         { 
//             this.fileName = filename;

//             this.csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
//             { 
//                 Encoding = Encoding.UTF8,
//                 Delimiter = ",",
//             };

//         }

//         public async Task<IEnumerable<Song>> ReadCSV() 
//         { 
//             using (var fs = File.Open(this.fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) 
//             { 
//                 using (var textReader = new StreamReader(fs,Encoding.UTF8))
//                 using (var csv = new CsvReader(textReader,this.csvConfiguration)) 
//                 {
//                     csv.Context.RegisterClassMap<SongClassMap>();

//                     // var data = csv.GetRecordsAsync();
                   
//                 }
//             }
//             return null;
            
//         }

//     }
// }