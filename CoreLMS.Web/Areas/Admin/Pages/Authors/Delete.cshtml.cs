using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreLMS.Core.Entities;
using CoreLMS.Persistence;
using CoreLMS.Core.Interfaces;
using CoreLMS.Core.DataTransferObjects;

namespace CoreLMS.Web.Areas.Admin.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IAuthorService authorService;

        public DeleteModel(IAuthorService authorService)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await authorService.DeleteAuthorAsync(AuthorDto.Id);

            return RedirectToPage("./Index");
        }
    }
}
