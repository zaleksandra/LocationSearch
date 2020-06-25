using Domain;
using Domain.Models;
using Repository;
using Repository.Interfaces;

namespace LocationSearch.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(LocationDatabaseContext context) : base(context)
        {
        }
    }
}
