namespace WebService.Repository.Context
{
    public class OverviewModel
    {
        public double tempMin { get; set; } 
        public double tempAvg { get; set; } 
        public double tempMax { get; set; } 
        public int humiMin { get; set; } 
        public int humiAvg { get; set; } 
        public int humiMax { get; set; } 
        public int co2Min { get; set; } 
        public int co2Avg { get; set; } 
        public int co2Max { get; set; }
    }
}