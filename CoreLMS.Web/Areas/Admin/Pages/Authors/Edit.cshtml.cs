using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreLMS.Core.Entities;
using CoreLMS.Persistence;
using CoreLMS.Core.Interfaces;
using CoreLMS.Core.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace CoreLMS.Web.Areas.Admin.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly IAuthorService authorService;

        public EditModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [BindProperty]
        public UpdateAuthorDto AuthorDto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author author;

            try
            {
                author = await authorService.GetAuthorAsync(id.Value);
            }
            catch (ApplicationException)
            {
                return NotFound();
            }

            AuthorDto = new UpdateAuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                MiddleName = author.MiddleName,
                LastName = author.LastName,
                Suffix = author.Suffix,
                ContactEmail = author.ContactEmail,
                ContactPhoneNumber = author.ContactPhoneNumber,
                Description = author.Description,
                WebsiteURL = author.WebsiteURL
            };

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await authorService.UpdateAuthorAsync(AuthorDto);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
            catch (Exception)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
