using Domain.Models;
using LocationSearch.Common.Helper;
using LocationSearch.Common.Models;
using LocationSearch.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using System;
using System.Linq;

namespace LocationSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICsvService _csvService;
        private readonly ILogger _logger;

        public LocationController(ILocationRepository locationRepository, ICsvService csvService, ILogger<LocationController> logger)
        {
            _locationRepository = locationRepository;
            _csvService = csvService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetLocation(double latitude, double longitude, double maxDistance, int maxResults)
        {
            try
            {
                var getValidLocations = _locationRepository
                    .Find(x => LocationHelper.InRadius(latitude, longitude, maxDistance, x))
                    .Select(x => new SearchResult {
                        Address = x.Address,
                        Distance = LocationHelper.CalculateDistance(x, new Location { Latitude = latitude, Longitude = longitude }) 
                    })
                    .OrderBy(x => x.Distance)
                    .Take(maxResults)
                    .ToList();


                return Ok(getValidLocations);
            }
            catch (Exception ex) {

                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

       

        [HttpPost]
        [Route("import")]
        [Consumes("application/json", "multipart/form-data")]
        public IActionResult ImportLocation(IFormFile file)
        {
            if (file == null)
            {
                _logger.LogWarning("The file was not send.");
                return BadRequest();
            }
            if (!file.ContentType.Contains("csv"))
            {
                _logger.LogWarning("Wrong file format.");
                return BadRequest();
            }
            var locatioList = _csvService.ReadLocation(file);

            var locations = _locationRepository.GetAll();
            try
            {
                foreach (var newLocation in locatioList)
                {
                    var existingLocation = locations.Where(x => x.Address == newLocation.Address).FirstOrDefault();
                    if (existingLocation == null)
                    {
                        var newLocationRecord = new Location() { Address = newLocation.Address, Latitude = newLocation.Latitude, Longitude = newLocation.Longitude };
                        var insertedLocation = _locationRepository.Insert(newLocationRecord); ;
                    }
                }
            }
            catch(Exception e) {
                _logger.LogError(e.Message);
            }
            return Ok();
            
        }

    }
}