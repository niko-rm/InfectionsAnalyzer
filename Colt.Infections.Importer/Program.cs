using Colt.Infections.Library.Entities;
using System;
using System.IO;
using System.Reflection;
using System.Linq;
using Colt.Infections.Importer.Models;
using System.Collections.Generic;

namespace Colt.Infections.Importer
{
    class Program
    {
        static string currentAssemblyPath = Colt.Infections.Application.GetApplicationRoot();
        static string downloadFolder = "Download";
        static string fileName = "full_data.csv";


        static void Main(string[] args)
        {
            args = new string[] { "Covid-19" };

            if (args.Length == 0)
                Console.WriteLine("You didn't set the virus code.");

            var virusCode = args[0];

            var csvFile = File.ReadAllLines(string.Join('\\', Program.currentAssemblyPath, downloadFolder, fileName));
            var listFromUpload = new List<CsvCovidInfection>();

            //Skipping first row: Titles 
            //0: date,
            //1: location,
            //2: new_cases,
            //3: new_deaths,
            //4: total_cases,
            //5: total_deaths

            foreach (var row in csvFile.Skip(1))
            {
                var eventDate = DateTime.MinValue;
                var newDefault = 0;
                var deathDefault = 0;

                var fields = row.Split(',');
                int.TryParse(fields[2], out newDefault);
                int.TryParse(fields[3], out deathDefault);
                DateTime.TryParse(fields[0], out eventDate);

                //ToDo: Check date for event date to filter (from last run to today)

                listFromUpload.Add(new CsvCovidInfection
                {
                    EventDate =eventDate,
                    Location = fields[1],
                    New = newDefault,
                    Death = deathDefault
                });
            }

            using (var ctx = new InfectionDbContext())
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
                foreach (var item in listFromUpload)
                {
                    virusDefinition.EventCaseDefinition.Add(new EventCaseDefinition()
                    {
                        DateEvent = item.EventDate,
                        Death = item.Death,
                        Infected = item.New,
                        Location = item.Location,
                        UidCase = Guid.NewGuid(),
                    });
                }

                ctx.SaveChanges();
            }
        }
    }
}
