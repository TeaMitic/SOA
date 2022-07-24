namespace MicroService_Gateway.Services.Urls 
{
    public static class ApiUrls
    { 
        // public const string DB_URL = "http://localhost:5000/api/songs/"; // for local
        public const string DB_URL = "http://service-db:5000/api/songs/"; //for docker
        public const string LYRICS_URL = "https://api.lyrics.ovh/v1/";
    }
}