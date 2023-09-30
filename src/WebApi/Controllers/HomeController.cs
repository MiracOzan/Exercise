using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        public DataContext _context;
        
        public HomeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetValue()
        {
            var values = await _context.City.ToListAsync();
            
            return Ok(values);
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var values = await _context.City
                .FirstOrDefaultAsync(v=>v.Id == id);
            
            return Ok(values);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }
    }
}
