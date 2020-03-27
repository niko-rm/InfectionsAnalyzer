using Colt.Infections.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Colt.Infections.Importer.Models.CovidInfectionItem;

namespace Colt.Infections.Importer
{
    public class UpdateData
    {

        public UpdateData()
        {

        }
        public void Update(IEnumerable<RawDataCovidInfection> rawData, string virusCode ) 
        {
            using (var ctx = new Library.Entities.InfectionDbContext())
            {
                //Check Virus Definition
                var virusDefinition = ctx.VirusDefinition.Where(x => x.VirusCode == virusCode).FirstOrDefault();
                if (virusDefinition == null)
                {
                    virusDefinition = new VirusDefinition()
                    {
                        VirusName = "Corona Virus",
                        VirusCode = virusCode,
                    };

                    ctx.VirusDefinition.Add(virusDefinition);
                }

                //add the list on database
                foreach (var item in rawData)
                {
                    virusDefinition.EventCaseDefinition.Add(new EventCaseDefinition()
                    {
                        DateEvent = item.Last_Update,
                        Death = item.Deaths,
                        Infected = item.Recovered,
                        Location = item.Country_Region,
                        UidCase = Guid.NewGuid(),
                    });
                }

                ctx.SaveChanges();
            }
        }
    }
}
