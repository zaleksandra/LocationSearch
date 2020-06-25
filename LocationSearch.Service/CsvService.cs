using CsvHelper;
using Domain.Models;
using LocationSearch.Common.Models;
using LocationSearch.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace LocationSearch.Service
{
    public class CsvService : ICsvService
    {
        public List<LocationImport> ReadLocation(IFormFile file)
        {
            TextReader reader = new StreamReader(file.OpenReadStream());
            CsvReader csvReader = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
            var locationRecords = csvReader.GetRecords<LocationImport>().ToList();

            return locationRecords;
        }
    }
}
