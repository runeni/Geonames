using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonames.Domain;
using Geonames.Models;
using Microsoft.AspNetCore.Mvc;

namespace Geonames.Controllers
{
    public class GeonamesController : Controller
    {
        private readonly IGeonamesProvider _geonamesProvider;

        public GeonamesController(IGeonamesProvider geonamesProvider)
        {
            _geonamesProvider = geonamesProvider;
        }

        // GET
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Geoname> geonames = new List<Geoname>();
            if (!string.IsNullOrEmpty(searchString)) geonames = await _geonamesProvider.GetGeonames(searchString);
            ViewData["Query"] = $"Søk på '{searchString}' returnerte {geonames.Count()} treff.";
            return View(geonames);
        }
    }
}