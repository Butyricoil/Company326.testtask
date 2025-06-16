using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cifkor.Karpusha
{
    [Serializable]
    public class Elevation
    {
        public string unitCode { get; set; }
        public double value { get; set; }
    }
    [Serializable]

    public class Geometry
    {
        public string type { get; set; }
        public List<List<List<double>>> coordinates { get; set; }
    }
    [Serializable]
    public class Period
    {
        public int number { get; set; }
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool isDaytime { get; set; }
        public int temperature { get; set; }
        public string temperatureUnit { get; set; }
        public string temperatureTrend { get; set; }
        public ProbabilityOfPrecipitation probabilityOfPrecipitation { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string icon { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }
    }
    [Serializable]
    public class ProbabilityOfPrecipitation
    {
        public string unitCode { get; set; }
        public int? value { get; set; }
    }
    [Serializable]
    public class Properties
    {
        public string units { get; set; }
        public string forecastGenerator { get; set; }
        public DateTime generatedAt { get; set; }
        public DateTime updateTime { get; set; }
        public string validTimes { get; set; }
        public Elevation elevation { get; set; }
        public List<Period> periods { get; set; }
    }
    [Serializable]
    public class RootWeather
    {
        [JsonProperty("@context")]
        public List<object> context { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }


}
