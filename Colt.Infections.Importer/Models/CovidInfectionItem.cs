using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Colt.Infections.Importer.Models
{
    public class CovidInfectionItem
    {
        public  SummaryStats summaryStats { get; set; }
        public  CacheCovidInfection cache { get; set; }
        public  DataSource dataSource { get; set; }
        public  string apiSourceCode { get; set; }
        public  IEnumerable<RawDataCovidInfection> rawData { get; set; }

        public class RawDataCovidInfection
        {
            public string FIPS { get; set; }
            public string Admin2 { get; set; }
            public string Province_State { get; set; }
            public string Country_Region { get; set; }
            public DateTime Last_Update { get; set; }
            public decimal Lat { get; set; }
            public decimal Long_ { get; set; }
            public int Confirmed { get; set; }
            public int Deaths { get; set; }
            public int Recovered { get; set; }
            public int Active { get; set; }
            public string Combined_Key { get; set; }
        }

        public class SummaryStats
        {
            public DataInfo global { get; set; }
            public DataInfo china { get; set; }
            public DataInfo nonChina { get; set; }
        }

        public class CacheCovidInfection
        {
            public string lastUpdated { get; set; }
            public string expires { get; set; }
            public string lastUpdatedTimestamp { get; set; }
            public string expiresTimeStamp { get; set; }
        }

        public class DataInfo
        {
            public int confirmed { get; set; }
            public int recovered { get; set; }
            public int deaths { get; set; }
        }
        public class DataSource
        {
            public string url { get; set; }
            public string lastGithubCommit { get; set; }
            public string publishedBy { get; set; }
            [JsonProperty(PropertyName = "ref")]
            public string _ref { get; set; }
        }
    }
}

