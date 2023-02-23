namespace Weather.Core.Dto
{
    public class WeatherDetail
    {
        public Coord Coord { get; set; }
        public List<Weather> Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Cloud Clouds { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }
    public class Coord
    {
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
    }
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
    public class Main
    {
        public decimal Temp { get; set; }
        public decimal Feels_like { get; set; }
        public decimal Temp_min { get; set; }
        public decimal Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Sea_level { get; set; }
        public int Grnd_level { get; set; }
    }
    public class Wind
    {
        public decimal Speed { get; set; }
        public decimal Deg { get; set; }
        public decimal Gust { get; set; }
    }
    public class Cloud {
        public int All { get; set; }
    }
    public class Sys
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public string Country { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}