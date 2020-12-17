namespace WebService.Repository.Context
{
    public class OverviewModel
    {
        public double tempMin { get; set; } 
        public double tempAvg { get; set; } 
        public double tempMax { get; set; } 
        public int humiMin { get; set; } 
        public double humiAvg { get; set; } 
        public int humiMax { get; set; } 
        public int co2Min { get; set; } 
        public double co2Avg { get; set; } 
        public int co2Max { get; set; }
    }
}