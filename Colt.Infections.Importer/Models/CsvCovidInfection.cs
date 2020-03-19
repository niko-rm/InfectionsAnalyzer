using System;
using System.Collections.Generic;
using System.Text;

namespace Colt.Infections.Importer.Models
{
    public class CsvCovidInfection
    {
        //0: date,
        //1: location,
        //2: new_cases,
        //3: new_deaths,
        //4: total_cases,
        //5: total_deaths

        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int New { get; set; }
        public int Death { get; set; }

    }
}
