using Domain.Models;
using LocationSearch.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationSearch.Service.Interfaces
{
    public interface ICsvService
    {
        List<LocationImport> ReadLocation(IFormFile file);
    }
}
