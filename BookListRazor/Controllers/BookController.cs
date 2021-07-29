using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json (new { data = await _db.Book.ToListAsync()});
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.Id == id );
            
            if (bookFromDb==null)
            {
                return NotFound();
            }

            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
