using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TestCatalog.Extensions;
using TestCatalog.Models;
using TestCatalog.Models.Custom;

namespace TestCatalog.Controllers
{
    [RoutePrefix("api/users")]
    public class UserApiController : ApiController
    {
        private readonly TestCatalogEntities _context;

        public UserApiController()
        {
            _context = new TestCatalogEntities();
        }

        [HttpGet]
        [Route("")]
        public async Task<Paged<User>> GetUsers([FromUri]Filter filter)
        {
            return await _context.Users
                .Include(u => u.Country)
                .Sort(filter)
                .Filter(filter)
                .ToPagedAsync(filter);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Country)
                .FirstOrDefaultAsync(u => u.ID == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateUser(int id, [FromBody]User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
                if (user == null)
                    return NotFound();

                if (user.FIO != model.FIO)
                    user.FIO = model.FIO;

                if (user.CountryID != model.CountryID)
                    user.CountryID = model.CountryID;

                if (user.Phone != model.Phone)
                    user.Phone = model.Phone;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("~/api/phones/generate")]
        public IHttpActionResult GeneratePhones()
        {
            return Ok(AppHelper.GeneratePhones());
        }

        [HttpGet]
        [Route("generate")]
        public async Task<IHttpActionResult> GenerateUsers()
        {
            if (_context.Users.Any())
                return Ok(await _context.Users.Include(c => c.Country).ToListAsync());

            try
            {
                var file = HttpContext.Current.Server.MapPath("~/Content/") + "names.txt";
                var countries = await _context.Countries.ToListAsync();
                var min = countries.Min(c => c.ID);
                var max = countries.Max(c => c.ID);
                var random = new Random();

                using (var reader = new StreamReader(file))
                {
                    while (true)
                    {
                        var line = reader.ReadLine();
                        if (line == null)
                            break;

                        if(string.IsNullOrEmpty(line))
                            continue;

                        _context.Users.Add(new User
                        {
                            FIO = line,
                            Phone = AppHelper.GeneratePhone(),
                            CountryID = countries.FirstOrDefault(c => c.ID == random.Next(min, max))?.ID ?? 1099
                        });
                    }

                    await _context.SaveChangesAsync();
                }

                return Ok(await _context.Users.Include(c => c.Country).ToListAsync());
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
