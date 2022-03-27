using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lista12.Data;
using Lista12.Models;

namespace Lista12.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly Lab12Context _context;

        public IndexModel(Lab12Context context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }
        public IList<Category> Categories { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Categories = await _context.Category.ToListAsync();
            if(id == null)
                Article = await _context.Article
                    .Include(a => a.Category)
                    .ToListAsync();
            else
                Article = await _context.Article
                    .Where((ar) => ar.CategoryId == id)
                    .Include(a => a.Category)
                    .ToListAsync();
        }
    }
}
