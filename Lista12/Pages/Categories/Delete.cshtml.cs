using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lista12.Data;
using Lista12.Models;

namespace Lista12.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly Lista12.Data.Lab12Context _context;

        public DeleteModel(Lista12.Data.Lab12Context context)
        {
            _context = context;
        }

        public string? ErrorInfo { get; set; }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FindAsync(id);

            if (Category != null)
            {
                int count = _context.Article.Where(ar => ar.CategoryId == Category.Id).Count();
                if (count > 0)
                {
                    ErrorInfo = "Cannot delete non empty category";
                    return Page();
                }
                _context.Category.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
