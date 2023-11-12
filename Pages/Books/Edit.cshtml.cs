using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adamescu_Georgiana_Lab2.Data;
using Adamescu_Georgiana_Lab2.Models;

namespace Adamescu_Georgiana_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Adamescu_Georgiana_Lab2.Data.Adamescu_Georgiana_Lab2Context _context;

        public EditModel(Adamescu_Georgiana_Lab2.Data.Adamescu_Georgiana_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //includem Author conform cu sarcina de la lab2

            Book =  await _context.Book
                .Include(b=>b.Publisher)
                .Include(b=>b.BookCategories)
                .ThenInclude(b=>b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            //apelam PopulateAssignedCategoryData pentru a obtine informatiile necesare checkbox-urilor folosind clasa AssignedCategoryData

            var authorList = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });

            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            //se va include Author conform cu sarcina de la lab2

            var bookToUpdate = await _context.Book
                .Include(i=>i.Publisher)
                .Include(i=>i.BookCategories)
                    .ThenInclude(i=>i.Category)
                .FirstOrDefaultAsync(s=>s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            //se va modifica Author ID conform cu sarcina de la lab 2

            if (await TryUpdateModelAsync<Book>(bookToUpdate,"Book",i=>i.Title, i=>i.Author, i=>i.Price, i=>i.PublishingDate, i=>i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            //apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care este editata

            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }
    }
}
