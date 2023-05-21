using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Model;

namespace WebScraper.Operations
{
    class SaveData
    {
        public SaveData(List<Laboratory> laboratories)
        {
            _laboratories = laboratories;

        }
        public List<Laboratory> _laboratories;
        public void SaveLaboratoryInformationToCSV()
        {
            string desktoppath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktoppath, "LaboatoriesInfo.csv");

            Parallel.ForEach(_laboratories, (item, state, index) =>
            {
                string line = $"{item.LaboratoryName},{item.Status},{item.Location},{item.ManagerName}," +
                    $"{item.AgentName},{"(+98)"} {item.TelephoneNumber}, {"//"} {item.FaxNumber},{item.WebsiteAddress},{item.Email}";

                lock (filePath) // Acquire the lock before writing to the file
                {
                    using (var writer = new StreamWriter(filePath, true, Encoding.UTF8))
                    {
                        writer.WriteLine(line);
                    }
                }
            });
            
        }


    }
}

