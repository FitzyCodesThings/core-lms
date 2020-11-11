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
using CoreLMS.Web.Areas.Admin.Models;

namespace CoreLMS.Web.Areas.Admin.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly IAuthorService authorService;

        public DetailsModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public AuthorViewModel Author { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbAuthor = await authorService.GetAuthorAsync(id.Value);

            Author = new AuthorViewModel
            {
                Id = dbAuthor.Id,
                DateCreated = dbAuthor.DateCreated,
                DateUpdated = dbAuthor.DateUpdated,
                FirstName = dbAuthor.FirstName,
                MiddleName = dbAuthor.MiddleName,
                LastName = dbAuthor.LastName,
                Suffix = dbAuthor.Suffix,
                ContactEmail = dbAuthor.ContactEmail,
                ContactPhoneNumber = dbAuthor.ContactPhoneNumber,
                Description = dbAuthor.Description,
                WebsiteURL = dbAuthor.WebsiteURL
            };

            if (Author == null)
            {
                return NotFound();
            }
            return Page();
        }
    }    
}
