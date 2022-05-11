using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using MicroService_Gateway.Models;

namespace MicroService_Gateway.CSVWrapper
{ 

    public class CsvHelperWrapper 
    { 
        private string fileName;
        private CsvConfiguration csvConfiguration;

        public CsvHelperWrapper(string filename) 
        { 
            this.fileName = filename;

            this.csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            { 
                Encoding = Encoding.UTF8,
                Delimiter = ",",
            };

        }

        public  IList<Song> ReadMoreRecords(int numOfLoaded, int chunkSize,ref bool  eof) 
        { 
            try
            {
                IList<Song> songs = new List<Song> ();
                using (var reader = new StreamReader(this.fileName))
                using (var csv = new CsvReader(reader, this.csvConfiguration)) 
                { 
                    csv.Context.RegisterClassMap<SongClassMap>();
                    csv.Read();
                    csv.ReadHeader();
                    for(int i = 0; i < numOfLoaded; i++) 
                    {
                        csv.Read();
                        csv.GetRecord<Song>();

                    }
                    while (songs.Count() < chunkSize ) 
                    { 
                        if (! csv.Read()) 
                        {
                            eof = true;
                            break;
                        }
                        var oneSong = csv.GetRecord<Song>();
                        songs.Add(oneSong);

                    }  
                }
                return songs;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}