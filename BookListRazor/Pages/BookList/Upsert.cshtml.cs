using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book book { get; set; }


        public async Task<IActionResult> OnGet(int? id)
        {
            book = new Book();
            if (id == null)
            {
                return Page();
            }
            book = await _db.Books.FindAsync(id);
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (book.Id == 0)
                {
                    _db.Books.Add(book);
                  
                }
                else
                {
                    _db.Books.Update(book);
                   
                }
                await _db.SaveChangesAsync();
                return Redirect("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
