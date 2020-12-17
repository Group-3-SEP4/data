using System.Collections.Generic;
using WebService.Repository.DAO;

namespace WebService.Repository.Context
{
    public class DetailedMeasurements
    {
        public List<DetailedCo2> detailedCo2List { get; set; }
        public List<DetailedHumidity> detailedHumidityList { get; set; }
        public List<DetailedTemperature> detailedTemperatureList { get; set; }
    }
}