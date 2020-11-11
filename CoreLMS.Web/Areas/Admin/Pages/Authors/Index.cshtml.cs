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
    public class IndexModel : PageModel
    {
        private readonly IAuthorService authorService;

        public IndexModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public IList<AuthorViewModel> Authors { get;set; }

        public async Task OnGetAsync()
        {
            var dbAuthors = await authorService.GetAuthorsAsync();

            this.Authors = dbAuthors.Select(p => new AuthorViewModel
            {
                Id = p.Id,
                DateCreated = p.DateCreated,
                DateUpdated = p.DateUpdated,
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                LastName = p.LastName,
                Suffix = p.Suffix,
                ContactEmail = p.ContactEmail,
                ContactPhoneNumber = p.ContactPhoneNumber,
                Description = p.Description,
                WebsiteURL = p.WebsiteURL                
            }).ToList();
        }
    }

    
}
