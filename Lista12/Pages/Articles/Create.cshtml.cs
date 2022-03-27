using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lista12.Data;
using Lista12.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Lista12.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly Lab12Context _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CreateModel(Lab12Context context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Article.Image != null)
            {
                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "upload");
                DirectoryInfo directoryInfo = new DirectoryInfo(uploadFolder);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Article.Name + ".jpg";
                FileStream fs = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.CreateNew, FileAccess.Write);
                Article.Image.CopyTo(fs);
                fs.Close();
                Article.ImagePath = Path.Combine("upload", fileName);
            }

            _context.Article.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
