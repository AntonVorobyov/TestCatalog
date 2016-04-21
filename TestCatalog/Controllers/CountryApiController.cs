using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TestCatalog.Extensions;
using TestCatalog.Models;
using TestCatalog.Models.Custom;
using WebGrease.Css.Extensions;

namespace TestCatalog.Controllers
{
    [RoutePrefix("api/countries")]
    public class CountryApiController : ApiController
    {
        private readonly TestCatalogEntities _context;

        public CountryApiController()
        {
            _context = new TestCatalogEntities();
        }

        [HttpGet]
        [Route("")]
        public async Task<Paged<Country>> GetCountries([FromUri]Filter filter)
        {
            return await _context.Countries
                .OrderBy(c => c.Title)
                .Filter(filter)
                .ToPagedAsync(filter);
        }

        [HttpGet]
        [Route("generate")]
        public async Task<IHttpActionResult> GenerateCountries()
        {
            if (_context.Countries.Any())
                return Ok(await _context.Countries.ToListAsync());

            var countries = AppHelper.GetCounties();
            try
            {
                countries
                    .Select(c => new Country { Title = c })
                    .ForEach(c => _context.Countries.Add(c));

                await _context.SaveChangesAsync();
                return Ok(countries);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            base.Dispose(disposing);
        }
    }
}