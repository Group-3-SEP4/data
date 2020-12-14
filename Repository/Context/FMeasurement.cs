﻿using System;
using System.Collections.Generic;

namespace WebService.Repository.Context.DatabaseSQL
{
    public partial class FMeasurement
    {
        public int MeasurementId { get; set; }
        public int DeviceDimKey { get; set; }
        public int TimeDimKey { get; set; }
        public int DateDimKey { get; set; }
        public int HumidityPercentage { get; set; }
        public int CarbonDioxide { get; set; }
        public double Temperature { get; set; }
        public int ServoPosition { get; set; }

        public virtual DateDim DateDimKeyNavigation { get; set; }
        public virtual DeviceDim DeviceDimKeyNavigation { get; set; }
        public virtual TimeDim TimeDimKeyNavigation { get; set; }
    }
}
