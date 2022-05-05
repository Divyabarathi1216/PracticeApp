using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> booklist { get; set; }

        public async Task OnGet()
        {
            booklist = await _db.Books.ToListAsync();

        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var dbook = await _db.Books.FindAsync(id);
            if (dbook != null)
            {
                _db.Books.Remove(dbook);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
