using Colt.Infections.Library.Entities;
using System;
using System.IO;
using System.Reflection;
using System.Linq;
using Colt.Infections.Importer.Models;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

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

            using (WebClient wc = new WebClient())
            {
                var url = "https://coronavirus.m.pipedream.net/";
                var json = wc.DownloadString(url);
                var dataInfection = JsonConvert.DeserializeObject<CovidInfectionItem>(json);

                UpdateData updateData = new UpdateData();

                updateData.Update(dataInfection.rawData, virusCode);


            }

        }
    }
}
