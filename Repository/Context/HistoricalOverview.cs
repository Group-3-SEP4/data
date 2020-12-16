using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Repository.Context
{
    public class HistoricalOverview
    {
        public double tempAvg { get; set; }   
        public int humiAvg { get; set; }   
        public int co2Avg { get; set; }
        public DateTime date { get; set; }
    }
}