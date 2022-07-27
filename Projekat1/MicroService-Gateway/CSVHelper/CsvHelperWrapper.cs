using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using MicroService_Gateway.Models;
using MicroService_Gateway.DTO;

namespace MicroService_Gateway.CSVWrapper
{ 

    public class CsvHelperWrapper 
    { 
        private string fileName;
        private CsvConfiguration csvConfiguration;
        public bool Eof { get; set; }

        public CsvHelperWrapper(string filename) 
        { 
            this.fileName = filename;

            this.csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            { 
                Encoding = Encoding.UTF8,
                Delimiter = ",",
            };

        }

        public IList<T> ReadMoreRecords<T,TMap>(int numOfLoaded, int chunkSize)  where TMap : ClassMap 
        { 
            try
            {
                IList<T> list = new List<T> ();
                using (var reader = new StreamReader(this.fileName))
                using (var csv = new CsvReader(reader, this.csvConfiguration)) 
                { 
                    csv.Context.RegisterClassMap<TMap>();
                    csv.Read();
                    csv.ReadHeader();
                    for(int i = 0; i < numOfLoaded; i++) 
                    {
                        csv.Read();
                        csv.GetRecord<T>();

                    }
                    while (list.Count() < chunkSize ) 
                    { 
                        if (! csv.Read()) 
                        {
                            this.Eof = true;
                            break;
                        }
                        var oneItem = csv.GetRecord<T>();
                        list.Add(oneItem);

                    }  
                }
                return list;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}